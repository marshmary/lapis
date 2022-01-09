<template>
  <content-wrapper>
    <image-list :data="images" :loading="loading" :errors="errors"></image-list>
    <div
      style="height: 1px"
      v-if="images.length"
      v-observe-visibility="handleScrolledToBottom"
    ></div>
  </content-wrapper>
</template>

<script setup>
import { useFetch } from "@/composable/useFetch";
import ContentWrapper from "@/components/ContentWrapper.vue";
import ImageList from "@/components/ImageList.vue";
import { onMounted, ref } from "vue";

const images = ref([]);
const pageNumber = ref(1);
const pageNumberLimit = ref(2);
const pageSize = ref(20);

const getUrl = () => {
  return `${process.env.VUE_APP_BACKEND_API}/images?pageNumber=${pageNumber.value}&pageSize=${pageSize.value}`;
};

const { fetch, loading, errors } = useFetch(getUrl(), {
  onCompleted: (res) => {
    images.value.push(...res.payload);

    pageNumberLimit.value =
      res.totalRecord % pageSize.value === 0
        ? Math.floor(res.totalRecord / pageSize.value)
        : Math.floor(res.totalRecord / pageSize.value) + 1;
  },
});

onMounted(() => {
  fetch();
});

const handleScrolledToBottom = (isVisible) => {
  if (!isVisible) return;

  if (pageNumber.value < pageNumberLimit.value && loading.value === false) {
    pageNumber.value++;
    fetch(getUrl());
  }
};
</script>

<style></style>
