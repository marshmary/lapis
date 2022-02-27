<template>
  <section class="image_list" v-if="props.errors">
    <not-found />
  </section>

  <section class="image_list" v-else-if="props.data.length">
    <div v-masonry transition-duration="0.25s" class="pt-4" item-selector=".item">
      <div class="row mx-0 pt-4">
        <div
          v-masonry-tile
          class="item col-12 col-md-6 col-lg-4 col-xxl-3"
          v-for="(item, index) in props.data"
          :key="index"
        >
          <image-list-item :image="item" class="mb-4" />
        </div>
      </div>
    </div>
  </section>

  <section class="image_list" v-else-if="props.data.length === 0 && props.loading === false">
    <not-found />
  </section>

  <section v-if="props.loading">
    <div class="pt-4 text-center">
      <div class="spinner-grow text-secondary" role="status">
        <span class="visually-hidden">Loading...</span>
      </div>
    </div>
  </section>
</template>

<script setup>
import { defineProps } from "@vue/runtime-core";

// components
import ImageListItem from "@/components/ImageListItem.vue";
import NotFound from "@/components/NotFound.vue";

const props = defineProps({
  data: Array,
  loading: Boolean,
  errors: String,
});
</script>

<style>
.image_list {
  min-height: calc(100vh - 56px);
}
</style>
