<template>
  <nav
    class="navbar navbar-expand-md fixed-top shadow_app"
    aria-label="Fourth navbar example"
  >
    <div class="container-fluid my-0 px-0">
      <router-link class="navbar-brand ms-3 text_color" to="/">Lapis</router-link>
      <button
        class="navbar-toggler me-2"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarsExample04"
        aria-controls="navbarsExample04"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <!-- <span class="navbar-toggler-icon text_color"></span> -->
        <font-awesome-icon class="navbar-toggler-icon text_color" icon="bars" />
      </button>

      <div class="collapse navbar-collapse" id="navbarsExample04">
        <ul class="navbar-nav ms-auto mb-2 mb-md-0">
          <li class="nav-item mx-3">
            <router-link class="nav-link text_color" to="/">Search</router-link>
          </li>
          <li class="nav-item mx-3">
            <router-link class="nav-link text_color" to="/about">About</router-link>
          </li>

          <!-- Login & Logout buttn -->
          <template v-if="userStore.isEmpty">
            <li class="nav-item mx-3">
              <router-link class="nav-link text_color" to="/login">Login</router-link>
            </li>
            <li class="nav-item mx-3">
              <router-link class="nav-link text_color" to="/signup">Sign up</router-link>
            </li>
          </template>

          <!-- Profile -->
          <template v-else>
            <li class="nav-item mx-3">
              <router-link class="nav-link text_color" to="/upload">Upload</router-link>
            </li>
            <li class="nav-item mx-3">
              <div class="btn-group">
                <a
                  class="nav-link dropdown-toggle text_color"
                  data-bs-toggle="dropdown"
                  aria-expanded="false"
                >
                  <img :src="userStore.avatar" alt="user avatar" class="user_avatar" />
                </a>
                <ul class="dropdown-menu dropdown-menu-end">
                  <li>
                    <router-link
                      class="dropdown-item text_color"
                      :to="`/user/${userStore.id}`"
                      >Profile</router-link
                    >
                  </li>
                  <li>
                    <p class="dropdown-item my-0 text_color" @click="logout">Logout</p>
                  </li>
                </ul>
              </div>
            </li>
          </template>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script setup>
// Store
import { useUserStore } from "@/store/useUser";

// Router
import { useRouter } from "vue-router";

// Declare router & store
const userStore = useUserStore();
const router = useRouter();

// Logout method
const logout = () => {
  // Clear user data
  userStore.logout();
  userStore.$reset();

  // Redirect to Home
  router.push("/");
};
</script>

<style scoped>
nav {
  max-height: 56px;
  background-color: var(--bg);
}

.fixed-top {
  z-index: 10;
}

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

.user_avatar {
  border-radius: 50%;
  height: 1.5rem;
  width: 1.5rem;
}

.text_color {
  color: var(--text-content) !important;
}

.text_color_opposite {
  color: var(--text-content-opp) !important;
}

.navbar-toggler:focus {
  box-shadow: none !important;
}

.navbar-collapse {
  background-color: var(--bg);
}
</style>
