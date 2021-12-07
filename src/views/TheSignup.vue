<template>
  <image-wrapper :imageurl="imageurl">
    <signup-form :imageurl="imageurl" />
  </image-wrapper>
</template>

<script setup>
import ImageWrapper from "../components/ImageWrapper.vue";
import SignupForm from "../components/SignupForm.vue";

let imageurl = "https://i.ibb.co/6XFhYM8/93490883-p0.jpg";
</script>

<style></style>
