<template>
    <nav
        class="navbar navbar-expand-md navbar-dark bg-dark fixed-top"
        aria-label="Fourth navbar example"
    >
        <div class="container-fluid">
            <a class="navbar-brand" href="#">Lapis</a>
            <button
                class="navbar-toggler"
                type="button"
                data-bs-toggle="collapse"
                data-bs-target="#navbarsExample04"
                aria-controls="navbarsExample04"
                aria-expanded="false"
                aria-label="Toggle navigation"
            >
                <span class="navbar-toggler-icon"></span>
            </button>

            <div class="collapse navbar-collapse" id="navbarsExample04">
                <ul class="navbar-nav ms-auto mb-2 mb-md-0">
                    <li class="nav-item mx-3">
                        <a class="nav-link" href="/">Home</a>
                    </li>
                    <li class="nav-item mx-3">
                        <a class="nav-link" href="/about">About</a>
                    </li>
                    <li class="nav-item mx-3">
                        <a class="nav-link" href="/login">Login</a>
                    </li>
                    <li class="nav-item mx-3">
                        <a class="nav-link" href="/signup">Sign up</a>
                    </li>

                    <li class="nav-item ms-3">
                        <div class="btn-group">
                            <a
                                class="nav-link dropdown-toggle"
                                data-bs-toggle="dropdown"
                                aria-expanded="false"
                            >
                                Profile
                            </a>
                            <ul class="dropdown-menu dropdown-menu-end">
                                <li>
                                    <a class="dropdown-item" href="#"
                                        >Menu item</a
                                    >
                                </li>
                                <li>
                                    <a class="dropdown-item" href="#"
                                        >Menu item</a
                                    >
                                </li>
                                <li>
                                    <a class="dropdown-item" href="#"
                                        >Menu item</a
                                    >
                                </li>
                            </ul>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
</template>

<script>
export default {};
</script>


<style scoped>
.bd-placeholder-img {
    font-size: 1.125rem;
    text-anchor: middle;
    -webkit-user-select: none;
    -moz-user-select: none;
    user-select: none;
}

@media (min-width: 768px) {
    .bd-placeholder-img-lg {
        font-size: 3.5rem;
    }
}
</style>