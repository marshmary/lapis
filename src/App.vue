<template>
  <div class="container-fluid px-0">
    <nav-bar />
    <router-view />
  </div>
</template>

<script>
import NavBar from "@/components/NavBar.vue";

export default {
  name: "App",
  components: {
    NavBar,
  },
};
</script>

<style>
body {
  margin: 0;
  padding: 0;
}

#app {
  font-family: "Lato", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}

.shadow_app {
  box-shadow: 0 3px 6px rgba(0, 0, 0, 0.08);
}

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

.tags-input-wrapper-default {
  padding: 0.5rem 0.75rem !important;
}

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

  --text-heading: #000;
  --text-content: #030303;
  --text-sub-content: #9d9c9d;

  --text-heading-opp: #fff;
  --text-content-opp: #fff;
  --text-sub-content-opp: #9d9c9d;
}

.text_heading {
  color: var(--text-heading);
}

.text_content {
  color: var(--text-content);
}

.text_sub_content {
  color: var(--text-sub-content);
}

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

::-webkit-scrollbar {
  width: 0; /* Remove scrollbar space */
  background: transparent; /* Optional: just make scrollbar invisible */
}
/* Optional: show position indicator in red */
::-webkit-scrollbar-thumb {
  background: #62656e;
}
</style>
