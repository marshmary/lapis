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
                v-for="image in data"
                :key="image.id"
            >
                <image-list-item :image="image" />
            </div>
        </div>
    </section>
</template>

<script>
import axios from "axios";
import ImageListItemLoading from "./ImageListItemLoading.vue";
import ImageListItem from "./ImageListItem.vue";

export default {
    name: "ImageList",
    components: {
        ImageListItemLoading,
        ImageListItem,
    },
    data() {
        return {
            data: null,
            loading: true,
            errors: false,
        };
    },
    mounted() {
        axios
            .get(`${process.env.VUE_APP_BACKEND_API}/images`)
            .then((response) => (this.data = response.data.payload))
            .catch((error) => {
                console.log(error);
                this.errors = true;
            })
            .finally(() => (this.loading = false));
    },
};
</script>

<style>
</style>