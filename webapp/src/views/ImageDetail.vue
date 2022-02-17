<script setup>
import ContentWrapper from "@/components/ContentWrapper.vue";
import CardImage from "@/components/CardImage.vue";
import TheLoading from "@/components/TheLoading.vue";
import NotFound from "@/components/NotFound.vue";
import InfiniteScrollImage from "@/components/InfiniteScrollImage.vue";
import { useRoute } from "vue-router";
import { useFetch } from "@vueuse/core";
import { API } from "@/helpers/Constants";

const route = useRoute();

const { data, isFetching, error } = useFetch(`${API}/images/${route.params.id}`).json();
</script>

<template>
  <content-wrapper minHeight="fit-content" id="imageCardContainer">
    <the-loading v-if="isFetching" />

    <not-found v-else-if="error" />

    <card-image :image="data" v-else />

    <h4 class="ps-2 mt-5" v-if="!isFetching">Related images</h4>
  </content-wrapper>

  <infinite-scroll-image marginTop="0px" />
</template>

<style lang="scss" scoped></style>
