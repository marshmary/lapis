<script setup>
import { computed, reactive, ref } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { required, url } from "@vuelidate/validators";
import TheModal from "@/components/TheModal.vue";
import { useCheckFile } from "@/helpers/useCheckFile";
import { useFetch } from "@/composable/useFetch";
import { usePalette } from "@/helpers/usePalette";
import { useHexColorConfig } from "@/helpers/useHexColorConfig";
import { useRouter } from "vue-router";

const modal = ref(undefined); // Modal to show error
const loading = ref(false); // Loading value
const hiddeninput = ref(undefined); // Input for load iamge
const uploadImage = ref(null); // Link to show image
const router = useRouter();
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

const rules = computed(() => ({
  form: {
    image: { required },
    credit: {
      author: { required },
      sourceUrl: { required, url },
    },
  },
}));

const v$ = useVuelidate(rules, state);

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

// Handle change in tag input
const onTagAdded = (slug) => {
  state.form.tags.push(slug.value);
};

const onTagRemoved = (slug) => {
  state.form.tags = state.form.tags.filter((tag) => tag !== slug.value);
};

// Submit new image
const onSubmit = async () => {
  loading.value = true;

  let formData = new FormData();
  formData.append("image", state.form.image);
  formData.append("colors.Primary", useHexColorConfig(state.form.colors.primary));
  formData.append("colors.Secondary", useHexColorConfig(state.form.colors.secondary));
  formData.append("colors.Tertiary", useHexColorConfig(state.form.colors.tertiary));
  formData.append("credit.Author", state.form.credit.author);
  formData.append("credit.SourceUrl", state.form.credit.sourceUrl);
  formData.append("credit.SourceUrl", state.form.credit.sourceUrl);
  if (state.form.tags.length !== 0) {
    for (let tag in state.form.tags) {
      formData.append("tags", state.form.tags[tag]);
    }
  }

  useFetch(`${process.env.VUE_APP_BACKEND_API}/images`, {
    method: "post",
    body: formData,
    onCompleted: (res) => {
      loading.value = false;
      state.modal.title = "Success";
      state.modal.body = res.message;
      state.modal.isError = false;
      modal.value.showModal();
      clearFormData();
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

const clearFormData = () => {
  router.go();
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
        <div v-if="uploadImage" class="drag_box">
          <img :src="uploadImage" class="upload_image border_app" />
          <div
            class="remove_button d-flex justify-content-center align-items-center"
            @click="handleRemoveUploadImage"
          >
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
            <h4>Details</h4>
          </div>
          <!-- Author -->
          <div class="col-12">
            <label for="inputCredit" class="form-label">Author</label>
            <input
              type="text"
              class="form-control"
              id="inputCredit"
              placeholder="Enter author name"
              v-model="v$.form.credit.author.$model"
            />
            <!-- error message -->
            <div
              class="input-errors"
              v-for="(error, index) of v$.form.credit.author.$errors"
              :key="index"
            >
              <div class="error-msg">{{ error.$message }}</div>
            </div>
          </div>

          <!-- Direct link -->
          <div class="col-12">
            <label for="inputSource" class="form-label">Source url</label>
            <input
              type="text"
              class="form-control"
              id="inputSource"
              placeholder="Enter source url"
              v-model="v$.form.credit.sourceUrl.$model"
            />
            <!-- Demo link -->
            <a
              v-if="
                state.form.credit.sourceUrl &&
                v$.form.credit.sourceUrl.$errors.length === 0
              "
              class="text_link"
              :href="state.form.credit.sourceUrl"
              target="_blank"
              >Try access</a
            >
            <!-- error message -->
            <div
              class="input-errors"
              v-for="(error, index) of v$.form.credit.sourceUrl.$errors"
              :key="index"
            >
              <div class="error-msg">{{ error.$message }}</div>
            </div>
          </div>

          <!-- Tag -->
          <div class="col-12">
            <label for="inputEmai" class="form-label">Tags</label>
            <tags-input
              element-id="imagetags"
              :add-tags-on-comma="true"
              @tag-added="onTagAdded"
              @tag-removed="onTagRemoved"
            ></tags-input>
            <!-- Small info -->
            <p class="text_subcontent">Enter a comma after each tag</p>
          </div>

          <!-- Submit -->
          <div class="col-12 mt-2 d-flex align-items-center">
            <button
              type="submit"
              class="btn btn-primary px-4 px-xl-5"
              :disabled="v$.form.$invalid || loading"
            >
              Create
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "@/scss/form.scss";

.wrapper {
  min-height: calc(100vh - 56px);
}

// Image upload
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

.text_subcontent {
  color: var(--text-sub-content);
  font-size: smaller;
}
</style>
