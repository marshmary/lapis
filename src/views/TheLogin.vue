<template>
  <image-wrapper :imageurl="imageurl">
    <login-form :imageurl="imageurl" />
  </image-wrapper>
</template>

<script setup>
import ImageWrapper from "@/components/ImageWrapper.vue";
import LoginForm from "@/components/LoginForm.vue";

let imageurl = "https://i.ibb.co/Z6DB73M/84908698-p1.jpg";
</script>

<style></style>
