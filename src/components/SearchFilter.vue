<script setup>
import { useSearchStore } from "@/store/useSearch";
import { TONES } from "@/helpers/Constants";

const searchStore = useSearchStore();
</script>

<template>
  <div class="search_filter p-3 d-flex shadow_app">
    <div class="me-auto">
      <span
        v-if="searchStore.tags.length > 0"
        class="badge rounded-pill bg-danger me-2 p-2"
        @click="searchStore.removeAllTag()"
      >
        <span class="ms-2 me-1">clear</span>
        <font-awesome-icon class="text_icon" icon="times" />
      </span>

      <span
        v-for="tag in searchStore.tags"
        :key="tag"
        class="badge rounded-pill bg-secondary me-2 p-2"
      >
        <span class="ms-2 me-1">#{{ tag }}</span>
        <font-awesome-icon
          class="text_icon"
          icon="times"
          @click="searchStore.removeTag(tag)"
        />
      </span>
    </div>

    <!-- Clear Orientation & Color button -->
    <a
      class="text-link text_sub_content me-3"
      role="button"
      v-if="searchStore.orientation !== '' || !searchStore.isColorEmpty"
      @click="searchStore.clearOrientationAndColor"
      >Clear</a
    >

    <!-- Orientation -->
    <div class="dropdown mx-3">
      <a
        class="dropdown-toggle text-link text_content"
        href="#"
        role="button"
        id="dropdownOrientation"
        data-bs-toggle="dropdown"
        aria-expanded="false"
        >{{
          searchStore.orientation === "" ? "Any orientation" : searchStore.orientation
        }}</a
      >
      <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="dropdownOrientation">
        <li @click="searchStore.setOrientation('')">
          <a class="dropdown-item">
            <p class="m-0 ms-2">Any orientation</p>
          </a>
        </li>
        <li @click="searchStore.setOrientation('Horizontal')">
          <a class="dropdown-item" href="#">
            <div class="d-flex align-items-center">
              <div class="d-flex align-items-center px-2">
                <div
                  style="
                    width: 18px;
                    height: 12px;
                    background: rgb(228, 228, 228);
                    border: 1px solid rgb(177, 177, 177);
                    box-sizing: border-box;
                    margin: 0px 10px 0px 0px;
                  "
                ></div>
                <p class="m-0 me-2">Landscape</p>
              </div>
            </div>
          </a>
        </li>
        <li @click="searchStore.setOrientation('Vertical')">
          <a class="dropdown-item" href="#">
            <div class="d-flex align-items-center px-2">
              <div
                style="
                  width: 18px;
                  height: 12px;
                  background: rgb(228, 228, 228);
                  border: 1px solid rgb(177, 177, 177);
                  box-sizing: border-box;
                  transform: rotate(90deg);
                  margin: 0px 10px 0px 0px;
                "
              ></div>
              <p class="m-0 me-2">Portrait</p>
            </div>
          </a>
        </li>
        <li @click="searchStore.setOrientation('Square')">
          <a class="dropdown-item" href="#">
            <div class="d-flex align-items-center px-2">
              <div
                style="
                  width: 18px;
                  height: 18px;
                  background: rgb(228, 228, 228);
                  border: 1px solid rgb(177, 177, 177);
                  box-sizing: border-box;
                  margin: 0px 10px 0px 0px;
                "
              ></div>
              <p class="m-0 me-2">Square</p>
            </div>
          </a>
        </li>
      </ul>
    </div>

    <!-- Color -->
    <div class="dropdown mx-3">
      <a
        class="dropdown-toggle text-link text_content"
        href="#"
        role="button"
        id="dropdownColors"
        data-bs-toggle="dropdown"
        aria-expanded="false"
        >{{ searchStore.isColorEmpty ? "Any color" : "Selected color" }}</a
      >
      <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="dropdownColors">
        <li>
          <a class="dropdown-item" @click="searchStore.removeColors">
            <p class="m-0 ms-2">Any color</p>
          </a>
        </li>
        <!-- Primary color -->
        <li>
          <div class="color_list">
            <div class="ms-2">Primary tones</div>
            <div class="d-flex flex-wrap mt-2 ms-1">
              <!-- Color list from Constants -->
              <div
                v-for="tone in TONES"
                :key="tone"
                class="color_items m-1 d-flex align-items-center justify-content-center"
                :class="searchStore.color.primary === tone ? 'selected' : ''"
                :style="{ backgroundColor: tone }"
                @click="searchStore.setPrimaryColor(tone)"
              >
                <!-- Check icon on selected color - Change color to white when background is black -->
                <font-awesome-icon
                  v-if="searchStore.color.primary === tone"
                  icon="check"
                  style="font-size: 0.5rem"
                  :style="tone === '#4D4C4C' ? { color: '#fff' } : ''"
                />
              </div>
            </div>
          </div>
        </li>
        <!-- Secondary color -->
        <li>
          <div class="color_list">
            <div class="ms-2">Secondary tones</div>
            <div class="d-flex flex-wrap mt-2 ms-1">
              <!-- Color list from Constants -->
              <div
                v-for="tone in TONES"
                :key="tone"
                class="color_items m-1 d-flex align-items-center justify-content-center"
                :class="searchStore.color.secondary === tone ? 'selected' : ''"
                :style="{ backgroundColor: tone }"
                @click="searchStore.setSecondaryColor(tone)"
              >
                <!-- Check icon on selected color - Change color to white when background is black -->
                <font-awesome-icon
                  v-if="searchStore.color.secondary === tone"
                  icon="check"
                  style="font-size: 0.5rem"
                  :style="tone === '#4D4C4C' ? { color: '#fff' } : ''"
                />
              </div>
            </div>
          </div>
        </li>
        <!-- Tertiary color -->
        <li>
          <div class="color_list">
            <div class="ms-2">Tertiary tones</div>
            <div class="d-flex flex-wrap mt-2 ms-1">
              <!-- Color list from Constants -->
              <div
                v-for="tone in TONES"
                :key="tone"
                class="color_items m-1 d-flex align-items-center justify-content-center"
                :class="searchStore.color.tertiary === tone ? 'selected' : ''"
                :style="{ backgroundColor: tone }"
                @click="searchStore.setTertiaryColor(tone)"
              >
                <!-- Check icon on selected color - Change color to white when background is black -->
                <font-awesome-icon
                  v-if="searchStore.color.tertiary === tone"
                  icon="check"
                  style="font-size: 0.5rem"
                  :style="tone === '#4D4C4C' ? { color: '#fff' } : ''"
                />
              </div>
            </div>
          </div>
        </li>
      </ul>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.search_filter {
  width: 100vw;
  max-height: 56px;
  position: fixed;
  left: 0;
  top: 56px;
  z-index: 10;
  background-color: var(--bg);
}

.dropdown {
  height: inherit;
}

.dropdown-toggle::after {
  margin-left: 0.5rem;
}

.color_list {
  padding: 0.25rem 1rem;
}

.color_items {
  border-radius: 50%;
  height: 1rem;
  width: 1rem;
  border: 0.01px solid rgba(0, 0, 0, 0.1);
}

.color_items.selected {
  box-shadow: 0 0 4px rgba(0, 0, 0, 0.3);
}

.text-link {
  text-decoration: none;
}

.text-link:hover {
  color: var(--text-content);
}

.text_op {
  color: #fff;
}
</style>
