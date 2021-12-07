<template>
  <div class="container-fluid px-0">
    <nav-bar />
    <router-view />
  </div>
</template>

<script>
import NavBar from "./components/NavBar.vue";

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
  padding-top: 56px;
}

#app {
  font-family: "Lato", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  background-color: var(--color-lightgray);
}

.shadow_app {
  box-shadow: 0 3px 7px rgba(0, 0, 0, 0.2);
}

.border_app {
  border-radius: 8px !important;
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
  --color-lightgray: #f4f6f9;

  --text-heading: #fff;
  --text-content: #fff;
  --text-sub-content: #fff;
}

.btn,
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
