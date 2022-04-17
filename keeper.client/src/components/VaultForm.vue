<template>
  <div class="bg-white p-5">
    <form>
      <div class="form-group">
        <label for="name" class="form-label mt-2">Vault name:</label>
        <input
          v-model="editable.name"
          type="name"
          class="form-control"
          id="vault-name"
        />
      </div>
      <div class="form-group">
        <label for="description" class="form-label mt-2">Description:</label>
        <input
          v-model="editable.description"
          type="name"
          class="form-control"
          id="vault-description"
        />
      </div>
      <div class="form-group">
        <label for="privacy" class="form-label mt-2">Privacy:</label><br />
        <select
          name="vault-isPrivate"
          v-model="editable.isPrivate"
          class="custom-select rounded"
          id="vault-isPrivate"
        >
          <option value="true">Private</option>
          <option value="false">Public</option>
        </select>
      </div>

      <button @click="createVault" type="submit" class="btn btn-success mt-3">
        Create
      </button>
      <!-- <button
        v-else
        @click="editvault"
        type="submit"
        class="btn btn-success mt-3"
      >
        Save Changes
      </button> -->
    </form>
  </div>
</template>


<script>
import { reactive, ref } from "@vue/reactivity";
import { vaultsService } from "../services/VaultsService"
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { Modal } from "bootstrap";
import { useRouter } from "vue-router";
import { watchEffect } from "@vue/runtime-core";
export default {
  props: {
    vault: {
      type: Object,
      required: false,
      default: {}
    }
  },
  setup(props) {
    const editable = ref({})
    const router = useRouter()
    watchEffect(() => {
      editable.value = props.vault
    })
    return {
      editable,
      createVault() {
        try {
          vaultsService.create(editable.value);
          editable.value = {};
          Modal.getOrCreateInstance(
            document.getElementById("vault-form")
          ).hide();
        } catch (error) {
          logger.error(error);
          Pop.toast(error.message, "error");
        }
      },
      // async editvault() {
      //   try {
      //     await vaultsService.update(editable.value);
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