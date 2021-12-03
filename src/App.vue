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
    min-height: 75rem;
    padding-top: 56px;
}

#app {
    font-family: "Lato", Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
}
</style>
