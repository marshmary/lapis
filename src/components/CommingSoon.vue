<script setup></script>

<template>
  <div class="d-flex flex-column justify-content-center align-items-center wrapper">
    <h1 class="text-wrap text-center fw-bold" style="width: 12rem">COMING SOON</h1>
    <p>We will lauch this page soon</p>
    <router-link class="btn btn-primary mt-2" to="/">Back to home</router-link>
  </div>
</template>

<style lang="scss" scoped>
@import "@/scss/form.scss";

.wrapper {
  width: 100%;
  min-height: calc(100vh - 56px);
}
</style>
