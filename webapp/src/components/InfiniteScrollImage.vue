<template>
  <content-wrapper :margin-top="props.marginTop ? props.marginTop : '56px'">
    <image-list :data="images" :loading="isFetching" :errors="error"></image-list>
    <div
      style="height: 1px"
      v-if="images.length"
      v-observe-visibility="handleScrolledToBottom"
    ></div>
  </content-wrapper>
</template>

<script setup>
// Libs
import { onMounted, ref } from "vue";
import { useFetch } from "@vueuse/core";
import { defineProps } from "@vue/runtime-core";

// Projs
import ContentWrapper from "@/components/ContentWrapper.vue";
import ImageList from "@/components/ImageList.vue";
import { API } from "@/helpers/Constants";

// Refs
const images = ref([]);
const pageNumber = ref(1);
const pageNumberLimit = ref(2);
const pageSize = ref(12);
const apiUrl = ref(
  `${API}/images?pageNumber=${pageNumber.value}&pageSize=${pageSize.value}`
);

const changeAPIUrl = () => {
  apiUrl.value = `${API}/images?pageNumber=${pageNumber.value}&pageSize=${pageSize.value}`;
};

const props = defineProps({
  marginTop: String,
});

//#region Fetch data
const { data, execute, isFetching, onFetchResponse, error } = useFetch(apiUrl, {
  refetch: true,
  immediate: false,
});

onFetchResponse(() => {
  let res = JSON.parse(data.value);

  images.value.push(...res.payload);

  pageNumberLimit.value =
    res.totalRecord % pageSize.value === 0
      ? Math.floor(res.totalRecord / pageSize.value)
      : Math.floor(res.totalRecord / pageSize.value) + 1;
});

//#endregion

// When component mounted
onMounted(() => {
  execute();
});

//#region Infinite scroll

const handleScrolledToBottom = (isVisible) => {
  if (!isVisible) return;

  if (pageNumber.value < pageNumberLimit.value && isFetching.value === false) {
    pageNumber.value++;
    changeAPIUrl();
  }
};

//#endregion
</script>
