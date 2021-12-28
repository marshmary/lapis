<template>
  <search-filter v-if="!searchStore.isEmpty" />
  <content-wrapper>
    <image-list :data="data" :loading="loading" :errors="errors" />
  </content-wrapper>
</template>

<script setup>
import { useSearchStore } from "@/store/useSearch";

import ContentWrapper from "@/components/ContentWrapper.vue";
import ImageList from "@/components/ImageList.vue";
import SearchFilter from "@/components/SearchFilter.vue";

import { useFetch } from "@/composable/useFetch";

const { data, loading, errors } = useFetch(`${process.env.VUE_APP_BACKEND_API}/images`);

const searchStore = useSearchStore();
</script>

<style></style>
