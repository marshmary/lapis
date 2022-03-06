<script setup>
import { defineProps } from "@vue/runtime-core";
import moment from "moment";

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

let marginX =
  props.image && props.image.orientation !== "Horizontal"
    ? "7rem !important"
    : "3rem !important";

const download = (url) => {
  fetch(url, { method: "GET" })
    .then((response) => response.blob())
    .then((blob) => {
      console.log(blob);
      const link = document.createElement("a");
      link.href = URL.createObjectURL(blob);
      link.download = blob.size;
      document.body.appendChild(link); // to work on Firefox
      link.click();
      URL.revokeObjectURL(link.href);
      link.remove();
    });
};
</script>

<template>
  <div class="d-flex justify-content-center align-items-center">
    <div
      class="card shadow_app border_app my-4"
      :style="{
        marginLeft: marginX,
        marginRight: marginX,
      }"
      v-if="image"
    >
      <div class="row g-0">
        <div class="col-12" :class="image.orientation !== 'Horizontal' ? 'col-md-7' : 'col-md-8'">
          <img :src="props.image.medium" class="img-fluid border_app_left" alt="image" />
        </div>
        <div class="col-12" :class="image.orientation !== 'Horizontal' ? 'col-md-5' : 'col-md-4'">
          <div class="card-body ms-1">
            <!-- Author -->
            <div class="card-title text-oposite d-flex justify-content-between">
              <div class="d-flex align-items-center">
                <a
                  :href="image.credit.sourceUrl"
                  target="_blank"
                  class="credit text_heading h5 fw-bold mb-0"
                >Author @{{ image.credit.author }}</a>
              </div>

              <!-- Download button group -->
              <div class="btn-group">
                <button type="button" class="btn bg-mint" @click="download(image.hight)">Download</button>
                <button
                  type="button"
                  class="btn bg-mint dropdown-toggle dropdown-toggle-split"
                  data-bs-toggle="dropdown"
                  aria-expanded="false"
                >
                  <span class="visually-hidden">Toggle Dropdown</span>
                </button>
                <ul class="dropdown-menu dropdown-menu-end">
                  <li>
                    <a
                      class="dropdown-item"
                      href="#"
                      @click.prevent="download(image.thumbnail)"
                    >Small</a>
                  </li>
                  <li>
                    <a class="dropdown-item" href="#" @click.prevent="download(image.medium)">Medium</a>
                  </li>
                  <li>
                    <a class="dropdown-item" href="#" @click.prevent="download(image.hight)">Large</a>
                  </li>
                </ul>
              </div>
            </div>

            <!-- Size and Orientation -->
            <p class="card-text">
              <span v-if="image.orientation === 'Horizontal'">
                <font-awesome-icon icon="image" />
              </span>
              <span v-else>
                <font-awesome-icon icon="file-image" />
              </span>

              <span class="ms-1">{{ image.size.width }} x {{ image.size.height }}</span>
            </p>

            <!-- Publish -->
            <p class="card-text">
              <small
                class="text-muted"
              >Published on {{ moment(image.created).format("MMMM DD, YYYY") }}</small>
            </p>

            <!-- Tag -->
            <p class="card-text">
              <span>
                <font-awesome-icon icon="tags" />
              </span>
              <span
                class="tag badge rounded-pill px-2 mx-1"
                v-for="tag in image.tags"
                :key="tag"
              >{{ tag }} {{ }}</span>
            </p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.card {
  border: none;
}

.img-fluid {
  min-height: 50vh;
  min-width: 30vw;
}

.bg-mint:hover {
  background-color: var(--color-mint-hight) !important;
}

.bg-mint:focus {
  background-color: var(--color-mint-hight) !important;
}

.card-body {
  height: 100%;
  background-color: var(--card-bg);
}

.credit {
  text-decoration: none;
}

.credit:hover {
  color: var(--color-mint-hight);
}

.text-align-center {
  text-align: center;
}

.tag {
  background-color: var(--color-mint-hight);
}
</style>
