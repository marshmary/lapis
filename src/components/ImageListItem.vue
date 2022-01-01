<template>
  <div class="card shadow_app border_app">
    <!-- Image -->
    <img
      @load="onImageLoad"
      :src="image.medium"
      class="card-img-top border_app"
      alt="image"
    />
    <!-- Content -->
    <div class="card-body border_app">
      <!-- Credit -->
      <h5 class="card-title text-oposite">
        @
        <a :href="image.credit.sourceUrl" target="_blank" class="credit">{{
          image.credit.author
        }}</a>
      </h5>

      <!-- Tag -->
      <!-- <p class="card-text">
        <span>
          <font-awesome-icon icon="tags" />
        </span>
        <span class="tag ms-1" v-for="tag in image.tags" :key="tag">#{{ tag }} {{}}</span>
      </p> -->

      <!-- Size and Orientation -->
      <!-- <p class="card-text">
        <span v-if="image.orientation === 'Horizontal'">
          <font-awesome-icon icon="image" />
        </span>
        <span v-else>
          <font-awesome-icon icon="file-image" />
        </span>

        <span class="ms-1">{{ image.size.width }} x {{ image.size.height }}</span>
      </p> -->

      <!-- Time -->
      <p class="card-text text-oposite">
        <font-awesome-icon icon="clock" />
        <span class="ms-1">{{ this.moment(image.created).fromNow() }}</span>
      </p>

      <!-- Download button -->
      <a class="btn btn-primary download_button" @click.prevent="download(image.hight)">
        <font-awesome-icon icon="download" />
      </a>
    </div>
  </div>
</template>

<script>
import moment from "moment";

export default {
  name: "ImageListItem",
  props: {
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
  },
  methods: {
    download(url) {
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
    },
  },
  beforeCreate() {
    this.moment = moment;
  },
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

.tag {
  color: var(--color-mint);
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
  right: 1rem;
  bottom: 1rem;
}

.card-title {
  position: absolute;
  left: 1rem;
  bottom: 1rem;
}

.border_app_top {
  border-top-left-radius: 8px;
  border-top-right-radius: 8px;
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
