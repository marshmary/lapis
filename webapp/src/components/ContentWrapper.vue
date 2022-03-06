<template>
  <div class="wrapper">
    <slot></slot>
  </div>
</template>

<script setup>
import { defineProps } from "@vue/runtime-core";

const props = defineProps({
  marginTop: String,
  minHeight: String,
});

let wrapper_margin_top = !props.marginTop ? "56px" : props.marginTop;
let min_height = !props.minHeight
  ? `calc(100vh - ${wrapper_margin_top})`
  : props.minHeight;
</script>

<style scoped>
.wrapper {
  width: 100%;
  min-height: v-bind(min_height);
  padding-left: 0.75rem;
  padding-right: 0.75rem;
  margin-top: v-bind(wrapper_margin_top);
  background-color: var(--bg);
}

@media only screen and (min-width: 768px) {
  .wrapper {
    padding-left: 4rem;
    padding-right: 4rem;
  }
}

@media only screen and (min-width: 1367px) {
  .wrapper {
    padding-left: calc(4rem + 5.5%);
    padding-right: calc(4rem + 5.5%);
  }
}
</style>
