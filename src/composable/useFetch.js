import axios from 'axios';
import { ref } from 'vue';
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
    const response = ref(null);
    const errors = ref(null);
    const loading = ref(false);

    const fetch = async (localUrl = "") => {
        loading.value = true;

        try {
            let res = null;

            let cachedData = Cache.get((localUrl === "") ? url : localUrl);
            if (cachedData) {
                data.value = cachedData;
            } else {
                if (localUrl === "") {
                    res = await axios(url, { ...options, data: options.body });
                } else {
                    res = await axios(localUrl, { ...options, data: options.body });
                }

                response.value = res;
                data.value = res.data;
                Cache.set((localUrl === "") ? url : localUrl, res.data);
            }

            options.onCompleted && options.onCompleted(data.value);
        } catch (error) {
            errors.value = error.response.data;

            options.onError && options.onError(error.response.data);
        } finally {
            loading.value = false;
        }
    }

    return { response, data, loading, errors, fetch }
}
