<template>
  <content-wrapper>
    <image-list :data="images" :loading="loading" :errors="errors" />
  </content-wrapper>
</template>

<script setup>
import { useFetch } from "@/composable/useFetch";
import ContentWrapper from "@/components/ContentWrapper.vue";
import ImageList from "@/components/ImageList.vue";
import { onMounted, onUnmounted, ref } from "vue";

const images = ref([]); //images list for render
const pageNumber = ref(1);
const pageNumberLimit = ref(1);
const pageSize = ref(20);

// First loading data and calculate pageLimit
const { loading, errors } = useFetch(
  `${process.env.VUE_APP_BACKEND_API}/images?pageNumber=${pageNumber.value}&pageSize=${pageSize.value}`,
  {
    onCompleted: (res) => {
      images.value.push(...res.payload);
      pageNumberLimit.value =
        res.totalRecord % pageSize.value === 0
          ? Math.floor(res.totalRecord / pageSize.value)
          : Math.floor(res.totalRecord / pageSize.value) + 1;
    },
  }
);

// Infinite scroll event and re-fetch data
onMounted(() => {
  window.addEventListener("scroll", handleScroll);
});

onUnmounted(() => {
  window.removeEventListener("scroll", handleScroll);
});

const handleScroll = () => {
  if (window.innerHeight + window.scrollY == document.body.offsetHeight + 56) {
    loadMoreImages();
  }
};

const loadMoreImages = () => {
  if (pageNumber.value < pageNumberLimit.value) {
    pageNumber.value++;
    useFetch(
      `${process.env.VUE_APP_BACKEND_API}/images?pageNumber=${pageNumber.value}&pageSize=${pageSize.value}`,
      {
        onCompleted: (res) => {
          images.value.push(...res.payload);
        },
      }
    );
  }
};
</script>

<style></style>
