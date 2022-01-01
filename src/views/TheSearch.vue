<template>
  <search-filter />
  <content-wrapper marginTop="112px">
    <image-list :data="images" :loading="loading" :errors="errors" />
  </content-wrapper>
</template>

<script setup>
// Vue
import { ref, watch, onMounted, onUnmounted } from "vue";

// Component
import ContentWrapper from "@/components/ContentWrapper.vue";
import ImageList from "@/components/ImageList.vue";
import SearchFilter from "@/components/SearchFilter.vue";
import { useFetch } from "@/composable/useFetch";
import { useSearchStore } from "@/store/useSearch";
import { useHexColorConfig } from "@/helpers/useHexColorConfig";

const searchStore = useSearchStore();

const apiUrl = ref(""); // url for fetching
const pageNumber = ref(1);
const pageSize = ref(20);
const pageNumberLimit = ref(1);
const images = ref([]); //Image list
const initialFetch = ref(false); // Prevent dupplicate fetch when moute
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

  return localApiUrl;
};

const RemovePageNumberAndPageSizeFromUrl = (api) => {
  return api.replace(`pageNumber=${pageNumber.value}&pageSize=${pageSize.value}`, "");
};

// First loading
const { loading, errors } = useFetch(generateAPIUrl(searchStore.tags), {
  onCompleted: (res) => {
    // Push new images to array
    images.value.push(...res.payload);

    // Set maxPageLimit
    pageNumberLimit.value =
      res.totalRecord % pageSize.value === 0
        ? Math.floor(res.totalRecord / pageSize.value)
        : Math.floor(res.totalRecord / pageSize.value) + 1;

    // Prevent duplicate fetch when moute
    initialFetch.value = true;

    // Compare API url for infinite scroll
    oldApiUrl.value = RemovePageNumberAndPageSizeFromUrl(apiUrl.value);
  },
});

const searchAndLoadMoreImage = (api) => {
  useFetch(api, {
    onCompleted: (res) => {
      // Add more images when the same fetch url
      if (oldApiUrl.value === RemovePageNumberAndPageSizeFromUrl(apiUrl.value)) {
        images.value.push(...res.payload);
      } else {
        // If not replace with new data
        images.value = res.payload;

        // Save old url for compare
        oldApiUrl.value = RemovePageNumberAndPageSizeFromUrl(apiUrl.value);

        // Reset page Number
        pageNumber.value = 1;

        // Set maxPageLimit
        pageNumberLimit.value =
          res.totalRecord % pageSize.value === 0
            ? Math.floor(res.totalRecord / pageSize.value)
            : Math.floor(res.totalRecord / pageSize.value) + 1;
      }
    },
  });
};

// Re-Fetch when ever user change search filter
watch(searchStore, () => {
  // Generate new url for fetch
  apiUrl.value = generateAPIUrl(
    searchStore.tags,
    searchStore.orientation,
    searchStore.color
  );

  // Re-generate url when diff url
  if (oldApiUrl.value !== RemovePageNumberAndPageSizeFromUrl(apiUrl.value)) {
    pageNumber.value = 1;
    apiUrl.value = generateAPIUrl(
      searchStore.tags,
      searchStore.orientation,
      searchStore.color
    );
  }

  // If not first time run then fetch
  if (initialFetch.value === true) {
    searchAndLoadMoreImage(apiUrl.value);
  }
});

// Infinite scroll event and re-fetch data
onMounted(() => {
  window.addEventListener("scroll", handleScroll);
});

onUnmounted(() => {
  window.removeEventListener("scroll", handleScroll);
});

const handleScroll = () => {
  if (window.innerHeight + window.scrollY == document.body.offsetHeight + 112) {
    loadMoreImages();
  }
};

const loadMoreImages = () => {
  if (pageNumber.value < pageNumberLimit.value) {
    pageNumber.value++;

    // Generate new url for fetch
    apiUrl.value = generateAPIUrl(
      searchStore.tags,
      searchStore.orientation,
      searchStore.color
    );

    searchAndLoadMoreImage(apiUrl.value);
  }
};
</script>

<style></style>
