<template>
  <div class="card shadow_app border_app">
    <!-- Image -->
    <img :src="`${props.image.medium}?`" class="card-img-top border_app" alt="image" />
    <!-- Content -->
    <div class="card-body border_app" @click.self="handleClickImage(image.id)">
      <!-- Credit -->
      <h5 class="card-title text-oposite">
        @
        <a :href="image.credit.sourceUrl" target="_blank" class="credit">{{
          image.credit.author
        }}</a>
      </h5>

      <!-- Time -->
      <p class="card-text text-oposite">
        <font-awesome-icon icon="clock" />
        <span class="ms-1">{{ moment(image.created).fromNow() }}</span>
      </p>

      <!-- Download button -->
      <a class="btn btn-primary download_button" @click.prevent="download(image.hight)">
        <font-awesome-icon icon="download" />
      </a>
    </div>
  </div>
</template>

<script setup>
import moment from "moment";
import { defineProps } from "@vue/runtime-core";
import { useRouter } from "vue-router";

const router = useRouter();

const props = defineProps({
  image: {
    id: String,
    thumbnail: String,
    medium: String,
    hight: String,
    tags: Array,
    orientation: String,
    size: Object,
    credit: Object,
    created: Date,
  },
});

const download = (url) => {
  fetch(url, { method: "GET" })
    .then((response) => response.blob())
    .then((blob) => {
      const link = document.createElement("a");
      link.href = URL.createObjectURL(blob);
      link.download = blob.size;
      document.body.appendChild(link); // to work on Firefox
      link.click();
      URL.revokeObjectURL(link.href);
      link.remove();
    });
};

const handleClickImage = (id) => {
  window.scrollTo(0, 0);
  router.push(`/image/${id}`);
};
</script>

<style scoped>
.credit {
  text-decoration: none;
  color: #fff;
}

.text-oposite {
  color: #fff;
}

.card {
  border: none;
  position: relative;
}

.card-img-top {
  opacity: 1;
  transition: 0.5s ease;
  backface-visibility: hidden;
}

.card-body {
  opacity: 0;
  transition: 0.5s ease;
  position: absolute;
  width: 100%;
  height: 100%;
  background: linear-gradient(
    rgba(3, 3, 3, 0.42),
    rgba(3, 3, 3, 0.09),
    rgba(3, 3, 3, 0.42)
  );
}

.card:hover .card-body {
  opacity: 1;
}

.download_button {
  position: absolute;
  z-index: 10;
  right: 1rem;
  bottom: 1rem;
}

.card-title {
  position: absolute;
  z-index: 10;
  left: 1rem;
  bottom: 1rem;
}

.btn-primary {
  background-color: var(--color-mint);
  border-color: var(--color-mint);
}

.btn-primary:hover {
  background-color: var(--color-mint-hight);
  border-color: var(--color-mint-hight);
}
</style>
