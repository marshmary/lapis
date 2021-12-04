<template>
    <div class="card shadow_app border_app" aria-hidden="true">
        <svg
            class="bd-placeholder-img card-img-top"
            width="100%"
            height="180"
            xmlns="http://www.w3.org/2000/svg"
            role="img"
            aria-label="Placeholder"
            preserveAspectRatio="xMidYMid slice"
            focusable="false"
        >
            <title>Placeholder</title>
            <rect width="100%" height="100%" fill="#868e96"></rect>
        </svg>
        <div class="card-body">
            <h5 class="card-title placeholder-glow">
                <span class="placeholder col-6"></span>
            </h5>
            <p class="card-text placeholder-glow">
                <span class="placeholder col-7"></span>
                <span class="placeholder col-4"></span>
                <span class="placeholder col-4"></span>
                <span class="placeholder col-6"></span>
                <span class="placeholder col-8"></span>
            </p>
            <a
                href="#"
                tabindex="-1"
                class="btn btn-primary disabled placeholder col-6"
            ></a>
        </div>
    </div>
</template>

<script>
export default {};
</script>

<style scope>
.card {
    border: none;
}

.btn-primary {
    background-color: var(--color-aqua);
    border-color: var(--color-aqua);
}
</style>