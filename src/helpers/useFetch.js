import axios from 'axios';
import { ref, unref } from 'vue';
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

export const useFetch = (url, options = {}) => {
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

    fetch();

    return { fetch, data, loading, errors }
}
