<template>
  <search-filter />
  <content-wrapper marginTop="112px">
    <image-list :data="data" :loading="loading" :errors="errors" />
  </content-wrapper>
</template>

<script setup>
// Vue
import { ref, watch } from "vue";

// Component
import ContentWrapper from "@/components/ContentWrapper.vue";
import ImageList from "@/components/ImageList.vue";
import SearchFilter from "@/components/SearchFilter.vue";
import { useFetch } from "@/composable/useFetch";
import { useSearchStore } from "@/store/useSearch";

const searchStore = useSearchStore();

const apiUrl = ref("");
const pageNumber = ref(1);
const pageSize = ref(24);

const generateAPIUrl = (
  tags = [],
  orientation = "",
  color = { primary: "", secondary: "", tertiary: "" }
) => {
  let apiUrl = `${process.env.VUE_APP_BACKEND_API}/images?pageNumber=${pageNumber.value}&pageSize=${pageSize.value}`;

  if (tags.length > 0) {
    for (const key in tags) {
      apiUrl += `&tags=${tags[key]}`;
    }
  }
  if (orientation !== "") {
    apiUrl += `&orientation=${orientation}`;
  }
  if (color.primary !== "") {
    console.log("🚀 ~ file: TheSearch.vue ~ line 42 ~ color.primary", color.primary);
    apiUrl += `&primaryColor=${color.primary}`;
  }
  if (color.secondary !== "") {
    console.log("🚀 ~ file: TheSearch.vue ~ line 46 ~ color.secondary", color.secondary);
    apiUrl += `&secondaryColor=${color.secondary}`;
  }
  if (color.tertiary !== "") {
    console.log("🚀 ~ file: TheSearch.vue ~ line 50 ~ color.tertiary", color.tertiary);
    apiUrl += `&tertiaryColor=${color.tertiary}`;
  }

  return apiUrl;
};

const { data, loading, errors, fetch } = useFetch(generateAPIUrl(searchStore.tags));

watch(searchStore, () => {
  apiUrl.value = generateAPIUrl(
    searchStore.tags,
    searchStore.orientation,
    searchStore.color
  );
  fetch(apiUrl.value);
});
</script>

<style></style>
