<template>
  <div class="form_box shadow_app border_app row mx-0">
    <teleport to="body">
      <the-modal ref="modal" :modalValue="state.modal"></the-modal>
    </teleport>
    <div class="d-none d-sm-block col-6 form_image border_app_left"></div>
    <div class="col-12 col-md-6 d-flex justify-content-center align-items-center px-5">
      <form class="row g-2" @submit.prevent="onSubmit">
        <div class="col-12">
          <h3>Login</h3>
        </div>

        <!-- Email -->
        <div class="col-12">
          <label for="inputEmai" class="form-label">Email</label>
          <input
            type="email"
            class="form-control"
            id="inputEmai"
            placeholder="Enter email"
            v-model="v$.form.email.$model"
          />
          <!-- error message -->
          <div
            class="input-errors"
            v-for="(error, index) of v$.form.email.$errors"
            :key="index"
          >
            <div class="error-msg">{{ error.$message }}</div>
          </div>
        </div>

        <!-- Password -->
        <div class="col-12">
          <label for="inputPassword" class="form-label">Password</label>
          <input
            type="password"
            class="form-control"
            id="inputPassword"
            placeholder="**********"
            v-model="v$.form.password.$model"
          />
          <!-- error message -->
          <div
            class="input-errors"
            v-for="(error, index) of v$.form.password.$errors"
            :key="index"
          >
            <div class="error-msg">{{ error.$message }}</div>
          </div>
        </div>

        <!-- Submit -->
        <div class="col-12 mt-4 d-flex align-items-center">
          <button
            type="submit"
            class="btn btn-primary px-4 px-xl-5"
            :disabled="v$.form.$invalid || loading"
          >
            Login
          </button>
          <div>
            <p class="ms-3 my-0">
              or
              <router-link class="ms-3 text_link" to="/signup">Sign up</router-link>
            </p>
          </div>
        </div>
        <div class="col-12 mt-3">
          <router-link class="text_link" to="/">Back to home</router-link>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup>
// Libs
import { ref, computed, reactive, defineProps } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { required, email, minLength } from "@vuelidate/validators";
import { useRouter } from "vue-router";

// Projs
import { useFetch } from "@/composable/useFetch";
import TheModal from "@/components/TheModal.vue";
import { useUserStore } from "@/store/useUser";

// Props for background image
const props = defineProps({
  imageurl: String,
});

const image = `url(${props.imageurl})`;

// Refs
const userStore = useUserStore(); // Store
const router = useRouter(); // Router to redirect
const modal = ref(undefined); // Modal to show error
const loading = ref(false); // Loading value

// Vuelidate
const state = reactive({
  form: {
    email: "",
    password: "",
  },
  modal: {
    title: "",
    body: "",
    isError: false,
  },
});
const requiredPasswordLength = ref(8);

const rules = computed(() => ({
  form: {
    email: { required, email },
    password: { required, min: minLength(requiredPasswordLength.value) },
  },
}));

const v$ = useVuelidate(rules, state);

// Submit form
const onSubmit = async () => {
  loading.value = true;

  useFetch(`${process.env.VUE_APP_BACKEND_API}/auth/login`, {
    method: "post",
    body: {
      email: state.form.email,
      password: state.form.password,
    },
    onCompleted: (res) => {
      loading.value = false;
      userStore.login(res);
      router.push("/");
    },
    onError: (errors) => {
      loading.value = false;
      // Show modal
      state.modal.title = "Error";
      state.modal.body = errors.message;
      state.modal.isError = true;
      modal.value.showModal();
    },
  });
};
</script>

<style lang="scss" scoped>
@import "@/scss/form.scss";

.form_image {
  background-image: v-bind(image);
  background-size: cover;
  background-position-y: 15%;
}
</style>
