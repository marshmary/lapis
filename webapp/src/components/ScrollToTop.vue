<script setup>
const handleScrollTop = () => {
  window.scrollTo(0, 0);
};
</script>

<template>
  <div
    class="scroll-top shadow_app d-flex justify-content-center align-items-center"
    @click="handleScrollTop"
  >
    <font-awesome-icon size="lg" icon="arrow-up" style="color: white" />
  </div>
</template>

<style lang="scss" scoped>
.scroll-top {
  position: fixed;
  background-color: var(--color-mint-hight);
  opacity: 0.8;
  border-radius: 50%;
  bottom: 1.5rem;
  right: 1rem;
  height: 3rem;
  width: 3rem;
  z-index: 10;
  cursor: pointer;
}
</style>
