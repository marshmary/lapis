<template>
  <content-wrapper>
    <image-list :data="data" :loading="loading" :errors="errors" />
  </content-wrapper>
</template>

<script setup>
import ContentWrapper from "@/components/ContentWrapper.vue";
import ImageList from "@/components/ImageList.vue";

import { useFetch } from "@/composable/useFetch";

const { data, loading, errors } = useFetch(`${process.env.VUE_APP_BACKEND_API}/images`);
</script>

<style></style>
