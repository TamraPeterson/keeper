<template>
  <div class="bg-white p-5">
    <form>
      <div class="form-group">
        <label for="name" class="form-label mt-2">Keep name:</label>
        <input
          v-model="editable.name"
          type="name"
          class="form-control"
          id="keep-name"
        />
      </div>
      <div class="form-group">
        <label for="description" class="form-label mt-2">Description:</label>
        <input
          v-model="editable.description"
          type="name"
          class="form-control"
          id="keep-description"
        />
      </div>
      <div class="form-group">
        <label for="img" class="form-label mt-2">Image:</label>
        <input
          v-model="editable.img"
          type="name"
          class="form-control"
          id="keep-img"
        />
      </div>

      <button @click="createKeep" type="submit" class="btn btn-primary mt-3">
        Create
      </button>
    </form>
  </div>
</template>


<script>
import { reactive, ref } from "@vue/reactivity";
import { keepsService } from "../services/KeepsService"
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { Modal } from "bootstrap";
import { useRouter } from "vue-router";
import { watchEffect } from "@vue/runtime-core";
export default {
  props: {
    keep: {
      type: Object,
      required: false,
      default: {}
    }
  },
  setup(props) {
    const editable = ref({})
    const router = useRouter()
    watchEffect(() => {
      editable.value = props.keep
    })
    return {
      editable,
      createKeep() {
        try {
          keepsService.create(editable.value);
          editable.value = {};
          Modal.getOrCreateInstance(
            document.getElementById("keep-form")
          ).hide();
        } catch (error) {
          logger.error(error);
          Pop.toast(error.message, "error");
        }
      },
      // async editkeep() {
      //   try {
      //     await keepsService.update(editable.value);
      //     // editable.value = {}
      //     Modal.getOrCreateInstance(document.getElementById("form-modal")).hide();
      //   } catch (error) {
      //     logger.error(error)
      //     Pop.toast(error.message, 'error')
      //   }
      // }
    }
  },
}
</script>


<style lang="scss" scoped>
.custom-select {
  width: 403px;
  height: 38px;
}
@media only screen and (max-width: 600px) {
  .custom-select {
    width: 270px;
  }
}
</style>