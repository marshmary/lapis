import axios from 'axios';
import { reactive } from 'vue';

export async function useFetch(url, options = {}) {
    const state = reactive({
        data: [],
        errors: undefined,
        loading: true,
    })

    try {
        const res = await axios(url, { ...options, data: options.body });
        state.data = res.data;

        options.onCompleted && options.onCompleted(res.data);
    } catch (errors) {
        state.errors = errors.response.data;

        options.onError && options.onError(errors.response.data);
    } finally {
        state.loading = false;
    }

    return state;
}
