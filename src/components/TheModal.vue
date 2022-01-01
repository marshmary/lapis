<template>
  <div class="modal fade" ref="exampleModal" tabindex="-1" aria-hidden="true">
    <div class="modal-dialog modal-sm modal-dialog-centered">
      <div class="modal-content border-0 border_app">
        <div class="modal-header justify-content-center border-0">
          <font-awesome-icon
            v-if="modalValue.isError"
            icon="exclamation-circle"
            class="heading_icon text_error mt-4 mx-0"
          />
          <font-awesome-icon
            v-else
            icon="check-circle"
            class="heading_icon text_info mt-4 mx-0"
          />
        </div>
        <div class="modal-body d-flex justify-content-center flex-column">
          <h4 class="text-center text_error" v-if="modalValue.isError">
            {{ modalValue.title }}
          </h4>
          <h4 class="text-center text_info" v-else>{{ modalValue.title }}</h4>
          <p class="text-center">{{ modalValue.body }}</p>
        </div>
        <div class="modal-footer border-0 justify-content-center">
          <button
            type="button"
            class="btn btn_error mb-4"
            @click="modal.hide()"
            v-if="modalValue.isError"
          >
            Try again
          </button>
          <button type="button" class="btn btn_info mb-4" @click="modal.hide()" v-else>
            Continue
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { Modal } from "bootstrap";

export default {
  name: "Modal",
  props: {
    modalValue: {
      title: String,
      body: String,
      isError: Boolean,
    },
  },
  data: () => ({
    modal: null,
  }),
  mounted() {
    this.modal = new Modal(this.$refs.exampleModal);
  },
  methods: {
    showModal() {
      this.modal.show();
    },
  },
};
</script>

<style lang="scss">
.heading_icon {
  font-size: 3rem;
}

.text_error {
  color: var(--color-grapefruit);
}

.btn_error {
  background-color: var(--color-grapefruit) !important;
  border-color: var(--color-grapefruit) !important;
  color: var(--text-content) !important;
}

.text_info {
  color: var(--color-aqua);
}

.btn_info {
  background-color: var(--color-aqua) !important;
  border-color: var(--color-aqua) !important;
  color: var(--text-content) !important;
}
</style>
