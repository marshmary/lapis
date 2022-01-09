import axios from 'axios';
import { ref, unref, watch } from 'vue';
import Cache from '@/helpers/cacheHandler';

// Add a request interceptor
axios.defaults.withCredentials = true;
axios.interceptors.request.use(
    function (config) {
        var token = localStorage.getItem("accessToken");
        config.headers.Authorization = token ? `Bearer ${token}` : "";
        return config;
    },
    function (error) {
        return Promise.reject(error);
    }
);

export const useFetch = (url, options = { immediate: true, refresh: false }) => {
    console.log("🚀 ~ file: useFetch.js ~ line 60 ~ useFetch ~ options", options)
    const data = ref(null);
    const errors = ref(null);
    const loading = ref(false);

    const fetch = async () => {
        loading.value = true;

        try {
            let res = null;

            let cachedData = Cache.get(url);
            if (cachedData) {
                data.value = cachedData;
            } else {
                res = await axios(unref(url), { ...options, data: options.body });
                data.value = res.data;
                Cache.set(url, res.data);
            }

            options.onCompleted && options.onCompleted(data.value);
        } catch (error) {
            errors.value = error.response.data;

            options.onError && options.onError(error.response.data);
        } finally {
            loading.value = false;
        }
    }

    if (options.immediate === true) {
        fetch();
    }

    watch(() => [
        unref(url),
        unref(options.refresh)
    ],
        () => { fetch() }, { deep: true });

    return { fetch, data, loading, errors }
}
