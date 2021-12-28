<template>
  <div class="wrapper">
    <slot></slot>
  </div>
</template>

<script setup>
import { useSearchStore } from "@/store/useSearch";
import { watch } from "vue";

const searchStore = useSearchStore();

let wrapper_padding = "56px";

watch(searchStore, () => {
  if (!searchStore.isEmpty) {
    wrapper_padding = "112px";
  } else {
    wrapper_padding = "56px";
  }
});
</script>

<style scoped>
.wrapper {
  width: 100%;
  min-height: calc(100vh - 56px);
  padding-left: 0.75rem;
  padding-right: 0.75rem;
  margin-top: v-bind(wrapper_padding);
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
