<template>
  <div class="wrapper">
    <div class="wrapper_bg"></div>
    <div class="content d-flex justify-content-center align-items-center">
      <slot></slot>
    </div>
  </div>
</template>

<script setup>
import { defineProps } from "@vue/runtime-core";

const props = defineProps({
  imageurl: String,
});

const image = `url(${props.imageurl})`;
</script>

<style lang="scss" scoped>
@import "@/scss/form.scss";
@import "@/scss/_mixins.scss";

.wrapper {
  @include wrapper-height();
  height: 100vh;
}

.wrapper_bg {
  @include wrapper-height;
  height: 100vh;
  @include fix-position();

  background-image: v-bind(image);
  background-size: cover;
  background-position-y: 15%;

  -webkit-filter: blur(10px);
  -moz-filter: blur(10px);
  -o-filter: blur(10px);
  -ms-filter: blur(10px);
  filter: blur(10px);
}

.content {
  @include wrapper-height;
  @include fix-position();
  height: 100vh;
}
</style>
