<template>
  <image-wrapper :imageurl="imageurl">
    <form-login :imageurl="imageurl" />
  </image-wrapper>
</template>

<script setup>
import ImageWrapper from "@/components/ImageWrapper.vue";
import FormLogin from "@/components/FormLogin.vue";

let imageurl = "https://i.ibb.co/Z6DB73M/84908698-p1.jpg";
</script>

<style></style>
