<template>
  <teleport to="body">
    <the-modal ref="modal" :modalValue="modal"></the-modal>
  </teleport>
  <div class="form_box shadow_app border_app row mx-0">
    <div class="col-12 col-md-6 d-flex justify-content-center align-items-center px-5">
      <form class="row g-2" @submit.prevent="onSubmit">
        <div class="col-12">
          <h3>Sign up</h3>
        </div>

        <!-- Fullname -->
        <div class="col-12">
          <label for="inputFullname" class="form-label">Fullname</label>
          <input
            type="text"
            class="form-control"
            id="inputFullname"
            placeholder="Enter fullname"
            v-model="v$.form.fullname.$model"
          />
          <!-- error message -->
          <div
            class="input-errors"
            v-for="(error, index) of v$.form.fullname.$errors"
            :key="index"
          >
            <div class="error-msg">{{ error.$message }}</div>
          </div>
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

        <!-- Re-Password -->
        <div class="col-12">
          <label for="inputRePassword" class="form-label">Re-Password</label>
          <input
            type="password"
            class="form-control"
            id="inputRePassword"
            placeholder="**********"
            v-model="v$.form.rePassword.$model"
          />
          <!-- error message -->
          <div
            class="input-errors"
            v-for="(error, index) of v$.form.rePassword.$errors"
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
            :disabled="v$.form.$invalid || submit.loading"
          >
            Sign up
          </button>
          <div>
            <p class="ms-3 my-0">
              or
              <router-link class="ms-3 text_link" to="/login">Login</router-link>
            </p>
          </div>
        </div>
      </form>
    </div>
    <div class="d-none d-sm-block col-6 form_image border_app_right"></div>
  </div>
</template>

<script>
// Validate libs
import useVuelidate from "@vuelidate/core";
import { required, email, minLength, sameAs, helpers } from "@vuelidate/validators";

// Composable
import { useFetch } from "@/composable/useFetch";
import TheModal from "@/components/TheModal.vue";

export default {
  props: {
    imageurl: String,
  },
  setup() {
    return { v$: useVuelidate() };
  },
  components: {
    TheModal,
  },
  data() {
    return {
      form: {
        fullname: "",
        email: "",
        password: "",
        rePassword: "",
      },
      submit: {
        data: undefined,
        loading: false,
        errors: undefined,
      },
      modal: {
        title: String,
        body: String,
        isError: Boolean,
      },
      image: `url(${this.imageurl})`,
    };
  },
  methods: {
    async onSubmit() {
      this.submit.loading = true;

      const res = await useFetch(`${process.env.VUE_APP_BACKEND_API}/user`, {
        method: "post",
        body: {
          email: this.form.email,
          password: this.form.password,
          name: this.form.fullname,
        },
        onCompleted: () => {
          this.$router.push("/login");
        },
        onError: (errors) => {
          // Show modal
          this.modal.title = "Error";
          this.modal.body = errors.message;
          this.modal.isError = true;
          this.$refs.modal.showModal();
        },
      });

      this.submit.data = res.data;
      this.submit.loading = res.loading;
    },
  },
  validations() {
    return {
      form: {
        fullname: { required },
        email: { required, email },
        password: { required, min: minLength(8) },
        rePassword: {
          required,
          sameAsPassword: helpers.withMessage(
            "Re-Password must be same as Password",
            sameAs(this.form.password)
          ),
          min: minLength(8),
        },
      },
    };
  },
};
</script>

<style scoped>
.form_box {
  width: 90%;
  min-height: 550px;
  background-color: #fff;
}

.form_image {
  background-image: v-bind(image);
  background-size: cover;
  background-position-y: 15%;
}

.text_link {
  color: var(--color-mint);
  text-decoration: none;
}

.text_link:hover {
  color: var(--color-mint-hight);
  text-decoration: underline;
}

.btn-primary {
  background-color: var(--color-mint);
  border-color: var(--color-mint);
}

.btn-primary:hover {
  background-color: var(--color-mint-hight);
  border-color: var(--color-mint-hight);
}

.input-errors {
  color: var(--color-grapefruit);
}

.error-msg {
  font-size: smaller;
}

@media (min-width: 1024px) {
  .form_box {
    width: 70%;
    height: 550px;
  }
}
</style>
