import axios from 'axios';
import { ref } from 'vue';

export const useFetch = (url, options = {}) => {
    const data = ref(null);
    const response = ref(null);
    const errors = ref(null);
    const loading = ref(false);

    const fetch = async () => {
        loading.value = true;
        try {
            const res = await axios(url, { ...options, data: options.body });
            response.value = res;
            data.value = res.data;

            options.onCompleted && options.onCompleted(res.data);
        } catch (error) {
            errors.value = error.response.data;

            options.onError && options.onError(error.response.data);
        } finally {
            loading.value = false;
        }
    }

    fetch()

    return { response, data, loading, errors, fetch }
}
