<template>
  <section v-if="props.errors">
    <p>Unable to find data</p>
  </section>

  <section v-else-if="props.loading">
    <div class="row mx-0 pt-4">
      <div class="col-12 col-md-6 col-lg-4 col-xxl-2 mb-4" v-for="n in 9" :key="n">
        <image-list-item-loading />
      </div>
    </div>
  </section>

  <section v-else>
    <div class="row mx-0 pt-4 imagecontainer" data-masonry>
      <div
        class="col-12 col-md-6 col-lg-4 col-xxl-3 mb-4"
        v-for="image in props.data.payload"
        :key="image.id"
      >
        <image-list-item :image="image" />
      </div>
    </div>
  </section>
</template>

<script setup>
import { onUpdated } from "vue";
import { defineProps } from "@vue/runtime-core";
import Masonry from "masonry-layout";
import imagesLoaded from "imagesloaded";

// components
import ImageListItemLoading from "@/components/ImageListItemLoading.vue";
import ImageListItem from "@/components/ImageListItem.vue";

const props = defineProps({
  data: Object,
  loading: Boolean,
  errors: Object,
});

onUpdated(() => {
  imagesLoaded(document.querySelector(".imagecontainer"), function () {
    if (props.loading === false) {
      var row = document.querySelector("[data-masonry]");
      new Masonry(row, {
        // options
        percentPosition: true,
      });
    }
  });
});
</script>

<style></style>
