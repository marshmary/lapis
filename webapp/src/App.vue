<template>
  <div :class="`${darkMode.isDarkMode ? 'dark' : ''} container-fluid px-0`">
    <nav-bar />
    <router-view :key="route.fullPath" />
    <scroll-to-top />
  </div>
</template>

<script setup>
import NavBar from "@/components/NavBar.vue";
import ScrollToTop from "@/components/ScrollToTop.vue";
import { useRoute } from "vue-router";
import { useDarkMode } from "@/store/useDarkMode";

const route = useRoute();
const darkMode = useDarkMode();

const localDarkMode = localStorage.getItem("isDarkMode");
darkMode.setDarkMode(localDarkMode ? localDarkMode : false);

</script>

<style>
/* default config */
body {
  margin: 0;
  padding: 0;
}

#app {
  font-family: "Lato", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}

/* shadow */

.shadow_app {
  box-shadow: 0 1px 20px rgba(0, 0, 0, 0.1);
}

/* border */

.border_app {
  border-radius: 8px !important;
}

.border_app_left {
  border-bottom-left-radius: 8px !important;
  border-top-left-radius: 8px !important;
}

.border_app_right {
  border-bottom-right-radius: 8px !important;
  border-top-right-radius: 8px !important;
}

.border_app_bottom {
  border-bottom-right-radius: 8px !important;
  border-bottom-left-radius: 8px !important;
}

/* Color for white mode & dark mode */

:root {
  --color-grapefruit: #eb5562;
  --color-bittersweet: #fb6c50;
  --color-sunflower: #f5b945;
  --color-grass: #89c152;
  --color-mint: #47cead;
  --color-mint-hight: #34ba9a;
  --color-aqua: #3badd8;
  --color-lavender: #ad93ea;
  --color-lightgray: #eeeeef;

  --bg: #fefeff;
  --card-bg: #fefeff;

  --nav-divider: #ffffff;
  --divider: #ced4da;

  --search-box-bg: #fefeff;
  --search-box-icon: #e9ecef;

  --upload-zone-bg: #eeeeef;

  --text-heading: #000;
  --text-content: #030303;
  --text-sub-content: #9d9c9d;

  --text-heading-opp: #fff;
  --text-content-opp: #fff;
  --text-sub-content-opp: #9d9c9d;
}

.dark {
  --color-grapefruit: #eb5562;
  --color-bittersweet: #fb6c50;
  --color-sunflower: #f5b945;
  --color-grass: #89c152;
  --color-mint: #47cead;
  --color-mint-hight: #34ba9a;
  --color-aqua: #3badd8;
  --color-lavender: #ad93ea;
  --color-lightgray: #eeeeef;

  --bg: #212121;
  --card-bg: rgb(51, 51, 51);

  --nav-divider: #3c3c3c;
  --divider: #3c3c3c;

  --search-box-bg: hsl(0, 0%, 7%);
  --search-box-icon: hsla(0, 0%, 100%, 0.08);

  --upload-zone-bg: hsla(0, 0%, 100%, 0.15);

  --text-heading: #fff;
  --text-content: #fff;
  --text-sub-content: #aaa;

  --text-heading-opp: #000;
  --text-content-opp: #030303;
  --text-sub-content-opp: #9d9c9d;
}

/* text styling */

.text_heading {
  color: var(--text-heading);
}

.text_content {
  color: var(--text-content);
}

.text_sub_content {
  color: var(--text-sub-content);
}

/* background styling */

.bg {
  background-color: var(--bg);
}

.card_bg {
  background-color: var(--card-bg);
}

.bg-mint {
  background-color: var(--color-mint) !important;
  color: var(--bg) !important;
}

/* restyling tags input */

.tags-input-wrapper-default {
  padding: 0.5rem 0.75rem !important;
  background-color: var(--search-box-bg) !important;
  border-color: var(--divider) !important;
  color: var(--text-sub-content) !important;
}

/* remove focus border of bootstrap */

.btn,
.btn:focus,
.form-select,
.form-select:active,
.form-select:focus,
.form-select:focus:active,
.form-control,
.form-control:active,
.form-control:focus,
.form-control:focus:active {
  outline: 0px !important;
  -webkit-appearance: none;
  box-shadow: none !important;
}

/* remove vertical scrollbar */

::-webkit-scrollbar {
  width: 0; /* Remove scrollbar space */
  background: transparent; /* Optional: just make scrollbar invisible */
}
/* Optional: show position indicator in red */
::-webkit-scrollbar-thumb {
  background: #62656e;
}

/* background color  */

.container-fluid {
  background-color: var(--bg);
}
</style>
