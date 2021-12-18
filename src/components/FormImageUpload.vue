<script setup>
import { reactive, ref } from "vue";
import TheModal from "@/components/TheModal.vue";
import { useCheckFile } from "@/helpers/useCheckFile";
import { usePalette } from "@/helpers/usePalette";

const modal = ref(undefined); // Modal to show error
const hiddeninput = ref(undefined); // Input for load iamge
const uploadImage = ref(null); // Link to show image
const { validateMessage, checkFile } = useCheckFile(); // Input file checker
const { primaryColor, secondaryColor, tertiaryColor, getCollorsFromFile } = usePalette(); // Get image palette

// Local state
const state = reactive({
  form: {
    image: null,
    tags: [],
    colors: {
      primary: "",
      secondary: "",
      tertiary: "",
    },
    credit: {
      author: "",
      sourceUrl: "",
    },
  },
  modal: {
    title: "",
    body: "",
    isError: false,
  },
  status: {
    over: false,
    dropped: false,
  },
});

// Upload image by dragging
const handleDragImage = () => {
  state.status.over = true;
};

const handleDropImage = (event) => {
  state.status.dropped = true;
  state.status.over = false;
  // Set upload image to form and display
  try {
    const fileItem = event.dataTransfer.items[0].getAsFile();
    var rs = checkFile(fileItem, "images");

    if (rs == false) {
      state.modal.title = "Error";
      state.modal.body = validateMessage;
      state.modal.isError = true;
      modal.value.showModal();
    } else {
      // update image to state
      uploadImage.value = URL.createObjectURL(fileItem);
      state.form.image = fileItem;

      // try to get color from image
      getCollorsFromFile(fileItem);
      state.form.colors.primary = primaryColor;
      state.form.colors.secondary = secondaryColor;
      state.form.colors.tertiary = tertiaryColor;
    }
  } catch {
    uploadImage.value = null;
  }
};

const handleDragLeaveImage = () => {
  state.status.over = false;
};

// Upload image using input element
const handleClickUpload = () => {
  hiddeninput.value.click();
};

const handleUploadImage = (event) => {
  try {
    const fileItem = event.target.files[0];
    var rs = checkFile(fileItem, "images");

    if (rs == false) {
      state.modal.title = "Error";
      state.modal.body = validateMessage;
      state.modal.isError = true;
      modal.value.showModal();
    } else {
      // Update state
      uploadImage.value = URL.createObjectURL(fileItem);
      state.form.image = fileItem;

      // Try to detect color palette from image
      getCollorsFromFile(fileItem);
      state.form.colors.primary = primaryColor;
      state.form.colors.secondary = secondaryColor;
      state.form.colors.tertiary = tertiaryColor;
    }
  } catch {
    uploadImage.value = null;
  }
};

// Remove uploaded image
const handleRemoveUploadImage = () => {
  uploadImage.value = null;
  state.form.image = null;
  state.form.colors.primary = "";
  state.form.colors.secondary = "";
  state.form.colors.tertiary = "";
};
</script>

<template>
  <div class="wrapper d-flex justify-content-center align-items-center">
    <!-- Modal -->
    <teleport to="body">
      <the-modal ref="modal" :modalValue="state.modal"></the-modal>
    </teleport>

    <!-- Form -->
    <div class="form_box shadow_app border_app row mx-0">
      <!-- Image upload -->
      <div class="col-6 d-flex justify-content-center align-items-center">
        <!-- Submitted image -->
        <div v-if="uploadImage" class="drag_box" @click="handleRemoveUploadImage">
          <img :src="uploadImage" class="upload_image border_app" />
          <div class="remove_button d-flex justify-content-center align-items-center">
            <font-awesome-icon icon="trash" size="2x" />
          </div>
        </div>
        <!-- Upload box -->
        <div
          class="drag_box border_app d-flex justify-content-center align-items-center"
          v-else
          @dragover.prevent="handleDragImage"
          @drop.prevent="handleDropImage"
          @dragleave.prevent="handleDragLeaveImage"
          @click="handleClickUpload"
        >
          <div
            class="border_dash border_app d-flex flex-column justify-content-center align-items-center text_upload"
          >
            <font-awesome-icon icon="arrow-circle-up" size="2x" />
            <p class="pt-3 text-wrap text-center" style="width: 70%">
              Drag and drop or click to upload file
            </p>
          </div>
        </div>
        <!-- Hidden input -->
        <input
          type="file"
          ref="hiddeninput"
          style="display: none"
          @change.prevent="handleUploadImage"
        />
      </div>
      <!-- Image content -->
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
            />
            <!-- v-model="v$.form.email.$model" -->
            <!-- error message -->
            <!-- <div
            class="input-errors"
            v-for="(error, index) of v$.form.email.$errors"
            :key="index"
          >
            <div class="error-msg">{{ error.$message }}</div>
            </div>-->
          </div>

          <!-- Submit -->
          <div class="col-12 mt-4 d-flex align-items-center">
            <button type="submit" class="btn btn-primary px-4 px-xl-5">
              <!-- :disabled="v$.form.$invalid || loading" -->
              Login
            </button>
            <div>
              <p class="ms-3 my-0">
                or
                <router-link class="ms-3 text_link" to="/signup">Sign up</router-link>
              </p>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
.wrapper {
  min-height: calc(100vh - 56px);
}

.form_box {
  width: 90%;
  min-height: 550px;
  background-color: #fff;
}

.drag_box {
  height: 85%;
  width: 80%;
  background-color: var(--color-lightgray);
  position: relative;
}

.border_dash {
  height: 93%;
  width: 88%;
  background-image: url("data:image/svg+xml,%3csvg width='100%25' height='100%25' xmlns='http://www.w3.org/2000/svg'%3e%3crect width='100%25' height='100%25' fill='none' stroke='%2300000038' stroke-width='4' stroke-dasharray='6%2c 9' stroke-dashoffset='0' stroke-linecap='square'/%3e%3c/svg%3e");
}

.upload_image {
  height: 100%;
  width: 100%;
  position: relative;
  object-fit: cover;
}

.remove_button {
  height: 3rem;
  width: 3rem;
  background-color: var(--bg);
  border-radius: 50%;
  position: absolute;
  z-index: 2;
  top: 46%;
  left: 46%;
}

.text_upload {
  color: var(--text-sub-content);
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
  border-color: var(--scolor-mint-hight);
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
