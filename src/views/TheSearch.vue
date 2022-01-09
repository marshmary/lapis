<template>
  <search-filter />
  <content-wrapper marginTop="112px">
    <image-list :data="images" :loading="loading" :errors="errors" />
    <div
      style="height: 1px"
      v-if="images.length"
      v-observe-visibility="handleScrolledToBottom"
    ></div>
  </content-wrapper>
</template>

<script setup>
// Vue
import { onMounted, ref, watch } from "vue";

// Component
import ContentWrapper from "@/components/ContentWrapper.vue";
import ImageList from "@/components/ImageList.vue";
import SearchFilter from "@/components/SearchFilter.vue";
import { useFetch } from "@/helpers/useFetch";
import { useSearchStore } from "@/store/useSearch";
import { useHexColorConfig } from "@/helpers/useHexColorConfig";

const searchStore = useSearchStore();

const apiUrl = ref(""); // url for fetching
const pageNumber = ref(1);
const pageSize = ref(20);
const pageNumberLimit = ref(2);
const images = ref([]); //Image list
const oldApiUrl = ref("");

const generateAPIUrl = (
  tags = [],
  orientation = "",
  color = { primary: "", secondary: "", tertiary: "" }
) => {
  let localApiUrl = `${process.env.VUE_APP_BACKEND_API}/images?pageNumber=${pageNumber.value}&pageSize=${pageSize.value}`;

  if (tags.length > 0) {
    for (const key in tags) {
      localApiUrl += `&tags=${tags[key]}`;
    }
  }
  if (orientation !== "") {
    localApiUrl += `&orientation=${orientation}`;
  }
  if (color.primary !== "") {
    localApiUrl += `&primaryColor=${useHexColorConfig(color.primary)}`;
  }
  if (color.secondary !== "") {
    localApiUrl += `&secondaryColor=${useHexColorConfig(color.secondary)}`;
  }
  if (color.tertiary !== "") {
    localApiUrl += `&tertiaryColor=${useHexColorConfig(color.tertiary)}`;
  }

  apiUrl.value = localApiUrl;

  return apiUrl.value;
};

const RemovePageNumberAndPageSizeFromUrl = (api) => {
  return api.replace(`pageNumber=${pageNumber.value}&pageSize=${pageSize.value}`, "");
};

const { fetch, loading, errors } = useFetch(generateAPIUrl(searchStore.tags), {
  onCompleted: (res) => {
    if (oldApiUrl.value === RemovePageNumberAndPageSizeFromUrl(apiUrl.value)) {
      images.value.push(...res.payload);
    } else {
      // If not replace with new data
      images.value = res.payload;

      // Save old url for compare
      oldApiUrl.value = RemovePageNumberAndPageSizeFromUrl(apiUrl.value);

      // Reset page Number
      pageNumber.value = 1;

      // Set maxPageLimitF
      pageNumberLimit.value =
        res.totalRecord % pageSize.value === 0
          ? Math.floor(res.totalRecord / pageSize.value)
          : Math.floor(res.totalRecord / pageSize.value) + 1;
    }
  },
});

onMounted(() => {
  console.log("mounted");
  fetch(generateAPIUrl(searchStore.tags, searchStore.orientation, searchStore.color));
});

// Re-Fetch when ever user change search filter
watch(searchStore, () => {
  pageNumber.value = 1;
  console.log("watched");
  fetch(generateAPIUrl(searchStore.tags, searchStore.orientation, searchStore.color));
});

const handleScrolledToBottom = (isVisible) => {
  if (!isVisible) return;

  if (pageNumber.value < pageNumberLimit.value && loading.value === false) {
    pageNumber.value++;
    fetch(generateAPIUrl(searchStore.tags, searchStore.orientation, searchStore.color));
  }
};
</script>

<style></style>
