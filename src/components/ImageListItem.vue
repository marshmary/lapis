<template>
    <div class="card shadow_app border_app">
        <!-- Image -->
        <img
            :src="image.medium"
            class="card-img-top border_app_top"
            alt="image"
        />

        <!-- Content -->
        <div class="card-body">
            <!-- Credit -->
            <h5 class="card-title">
                Credit to
                <a
                    :href="image.credit.sourceUrl"
                    target="_blank"
                    class="credit"
                    >{{ image.credit.author }}</a
                >
            </h5>

            <!-- Tag -->
            <p class="card-text">
                <font-awesome-icon icon="tags" />
                <span class="tag" v-for="tag in image.tags" :key="tag">
                    #{{ tag }}
                </span>
            </p>

            <!-- Size and Orientation -->
            <p class="card-text">
                <span v-if="image.orientation === 'Horizontal'">
                    <font-awesome-icon icon="image" />
                </span>
                <span v-else>
                    <font-awesome-icon icon="file-image" />
                </span>

                <span>{{ image.size.width }} x {{ image.size.height }}</span>
            </p>

            <!-- Time -->
            <p class="card-text">
                <font-awesome-icon icon="clock" />
                {{ this.moment(image.created).fromNow() }}
            </p>

            <!-- Download button -->
            <a class="btn btn-primary" @click.prevent="download(image.hight)"
                ><font-awesome-icon icon="download" /> Download</a
            >
        </div>
    </div>
</template>

<script>
import axios from "axios";
import moment from "moment";

export default {
    name: "ImageListItem",
    props: {
        image: {
            id: String,
            thumbnail: String,
            medium: String,
            hight: String,
            tags: Array,
            orientation: String,
            size: Object,
            credit: Object,
            created: Date,
        },
    },
    methods: {
        download(url) {
            axios.get(url, { responseType: "blob" }).then((response) => {
                const blob = response.data;
                const link = document.createElement("a");
                link.href = URL.createObjectURL(blob);
                link.download = blob.size;
                link.click();
                URL.revokeObjectURL(link.href);
            });
        },
    },
    beforeCreate() {
        this.moment = moment;
    },
};
</script>

<style scoped>
.credit {
    text-decoration: none;
    color: var(--color-mint);
}

.tag {
    color: var(--color-mint);
}

.card {
    border: none;
}

.border_app_top {
    border-top-left-radius: 8px;
    border-top-right-radius: 8px;
}

.btn-primary {
    background-color: var(--color-mint);
    border-color: var(--color-mint);
}

.btn-primary:hover {
    background-color: var(--color-mint-hight);
    border-color: var(--color-mint-hight);
}
</style>