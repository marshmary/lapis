<template>
  <image-wrapper :imageurl="imageurl">
    <form-signup :imageurl="imageurl" />
  </image-wrapper>
</template>

<script setup>
import ImageWrapper from "@/components/ImageWrapper.vue";
import FormSignup from "@/components/FormSignup.vue";

let imageurl = "https://i.ibb.co/fr15Rrb/93490883-p0.jpg";
</script>

<style></style>
