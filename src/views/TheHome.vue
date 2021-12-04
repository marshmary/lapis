<template>
    <content-wrapper>
        <image-list />
    </content-wrapper>
</template>

<script>
import ContentWrapper from "../components/ContentWrapper.vue";
import ImageList from "../components/ImageList.vue";

export default {
    name: "TheHome",
    components: {
        ContentWrapper,
        ImageList,
    },
};
</script>

<style>
</style>