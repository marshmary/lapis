<template>
  <content-wrapper :margin-top="props.marginTop ? props.marginTop : '56px'">
    <image-list :data="images" :loading="isFetching" :errors="error" />
    <div style="height: 3px" v-observe-visibility="handleScrolledToBottom"></div>
  </content-wrapper>
</template>

<script setup>
// Libs
import { ref, watch } from "vue";
import { useFetch, useWindowScroll } from "@vueuse/core";
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
const firstLoad = ref(false); // Control if the first load is done for scrolling

const props = defineProps({
  marginTop: String,
});

//#region Fetch data

const { data, isFetching, onFetchResponse, error } = useFetch(apiUrl, {
  refetch: true,
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

const changeAPIUrlToReFetch = () => {
  if (pageNumber.value < pageNumberLimit.value && isFetching.value === false) {
    pageNumber.value++;
    apiUrl.value = `${API}/images?pageNumber=${pageNumber.value}&pageSize=${pageSize.value}`;
  }
};

//#region Fetch when scroll a little bit

const { y } = useWindowScroll();

watch(y, (newValue, oldValue) => {
  if (newValue > oldValue + 40) {
    changeAPIUrlToReFetch();
    firstLoad.value = true;
  }
});

//#endregion

//#region Infinite scroll

const handleScrolledToBottom = (isVisible) => {
  if (!isVisible) return;

  if (firstLoad.value === true) {
    changeAPIUrlToReFetch();
  }
};

//#endregion
</script>
