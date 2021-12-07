<template>
    <div class="wrapper">
        <slot></slot>
    </div>
</template>

<script>
export default {};
</script>

<style scoped>
.wrapper {
    width: 100%;
    min-height: calc(100vh - 56px);
    padding-left: 0.75rem;
    padding-right: 0.75rem;
    padding-top: 1rem;
}

@media only screen and (min-width: 768px) {
    .wrapper {
        padding-left: 4rem;
        padding-right: 4rem;
    }
}

@media only screen and (min-width: 1367px) {
    .wrapper {
        padding-left: calc(4rem + 5.5%);
        padding-right: calc(4rem + 5.5%);
    }
}
</style>