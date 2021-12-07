<template>
    <section v-if="errors">
        <p>Unable to find data</p>
    </section>

    <section v-if="loading">
        <div class="row mx-0">
            <div
                class="col-12 col-md-4 col-lg-3 col-xxl-2 mb-4"
                v-for="n in 8"
                :key="n"
            >
                <image-list-item-loading />
            </div>
        </div>
    </section>

    <section v-else>
        <div
            class="row mx-0 scrolling_component"
            ref="scrollComponent"
            data-masonry='{"percentPosition": true }'
        >
            <div
                class="col-12 col-md-4 col-lg-3 col-xxl-2 mb-4"
                v-for="image in data.payload"
                :key="image.id"
            >
                <image-list-item :image="image" />
            </div>
        </div>
    </section>
</template>

<script>
// libs
import { ref, onMounted } from "vue";

// components
import ImageListItemLoading from "./ImageListItemLoading.vue";
import ImageListItem from "./ImageListItem.vue";

// composable
import { useFetch } from "../composable/use-fetch";

export default {
    name: "ImageList",
    setup() {
        const data = ref([]);
        const loading = ref(true);
        const errors = ref(undefined);

        onMounted(async () => {
            const res = await useFetch(
                `${process.env.VUE_APP_BACKEND_API}/images`
            );
            data.value = res.data;
            loading.value = res.loading;
            errors.value = res.errors;
        });

        return { data, loading, errors };
    },
    components: {
        ImageListItemLoading,
        ImageListItem,
    },
};
</script>

<style>
</style>