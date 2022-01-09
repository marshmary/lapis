<template>
  <nav
    v-if="isShow"
    class="navbar navbar-expand-md fixed-top border-bottom"
    aria-label="Fourth navbar example"
  >
    <div class="container-fluid my-0 px-0">
      <router-link class="navbar-brand ms-3 text_color" to="/">Lapis</router-link>
      <button
        class="navbar-toggler me-2"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarshidden"
        aria-controls="navbarshidden"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <!-- <span class="navbar-toggler-icon text_color"></span> -->
        <font-awesome-icon class="navbar-toggler-icon text_color" icon="bars" />
      </button>

      <div class="collapse navbar-collapse" id="navbarshidden">
        <form class="search_form">
          <div class="input-group">
            <span class="input-group-text px-3 border_app_left" id="basic-addon1">
              <font-awesome-icon class="text_icon" icon="search" />
            </span>
            <input
              type="search"
              class="form-control border_app_right"
              placeholder="Search images"
              @focus="onFocus"
              @blur="outFocus"
              @keydown.enter.prevent="onEnter"
            />
          </div>
        </form>

        <ul class="navbar-nav mb-2 mb-md-0 ms-auto">
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
import { watchEffect, ref } from "vue";

// Store
import { useUserStore } from "@/store/useUser";
import { useSearchStore } from "@/store/useSearch";

// Router
import { useRouter } from "vue-router";

// Declare router & store
const userStore = useUserStore();
const searchStore = useSearchStore();
const router = useRouter();

const isShow = ref(true);

watchEffect(() => {
  if (
    router.currentRoute.value.name === "Login" ||
    router.currentRoute.value.name === "Signup"
  ) {
    isShow.value = false;
  } else {
    isShow.value = true;
  }
});

// Logout method
const logout = () => {
  // Clear user data
  userStore.logout();
  userStore.$reset();

  // Redirect to Home
  router.push("/");
};

// Tags input
const onEnter = (e) => {
  searchStore.setTags(e.target.value);
  e.target.value = "";
  router.push("/search");
};
</script>

<style scoped>
nav {
  max-height: 56px;
  background-color: var(--bg);
}

.fixed-top {
  z-index: 12;
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

.search_form {
  width: 35rem;
  margin-left: 1rem;
}

@media screen and (max-width: 768px) {
  .search_form {
    width: 20rem;
  }
}
</style>
