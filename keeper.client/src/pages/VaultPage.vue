<template>
  <div class="component">
    <div class="row p-md-5 align-items-center">
      <div class="col-md-6">
        <h1 class>{{ activeVault?.name }}</h1>
        <h4>{{ activeVault.description }}</h4>
        <h5 class="mt-3">Keeps: {{ keeps.length }}</h5>
      </div>
      <div class="col-md-6 text-end">
        <button
          v-if="account.id == activeVault.creatorId"
          class="btn btn-primary"
          @click="deleteVault(activeVault)"
        >
          Delete Vault
        </button>
      </div>
    </div>
    <div class="container">
      <div class="masonry-with-columns">
        <div
          class="bg-white m-3 rounded shadow card"
          v-for="k in keeps"
          :key="k.id"
        >
          <i
            title="delete"
            class="mdi mdi-delete top-right selectable trash"
            @click="removeFromVault(k.vaultKeepId)"
          ></i>
          <img class="img img-fluid rounded" :src="k.img" alt="" />
          <div class="bottom-left">
            <h5 class="keepname">{{ k.name }}</h5>
          </div>
          <img
            class="img img-fluid avatar bottom-right mb-1"
            :src="k.creator?.picture"
            alt=""
          />
        </div>
      </div>
    </div>
  </div>
  <Modal id="keep-details">
    <template #modal-body><KeepDetails /></template>
  </Modal>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { useRoute } from "vue-router"
import { onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import { keepsService } from "../services/KeepsService"
import { vaultsService } from "../services/VaultsService"
import Pop from "../utils/Pop"
import { router } from "../router"
import { Modal } from "bootstrap"
import { vaultKeepsService } from "../services/VaultKeepsService"
export default {

  setup() {
    const route = useRoute();
    onMounted(async () => {
      AppState.keeps = []
      vaultsService.getById(route.params.id)
      vaultsService.getVaultKeeps(route.params.id)
      logger.log('found vault', route.params.id)

    })
    return {
      activeVault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      vaultKeeps: computed(() => AppState.vaultKeeps),

      async deleteVault(vault) {
        try {
          if (await Pop.confirm("Are you sure you want to delete this vault?")) {
            debugger
            const userId = vault.creator.id
            vaultsService.deleteVault(vault.id)
            router.push({ name: "Profile", params: { id: userId } })
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async setActive(keep) {
        try {
          await keepsService.getById(keep.id)
          await vaultsService.getVaultKeeps(keep.id)
          Modal.getOrCreateInstance(document.getElementById("keep-details")).show();
          logger.log('active keep', keep)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async removeFromVault(id) {
        try {
          if (await Pop.confirm("Are you sure you want to remove this from your vault?")) {
            await vaultKeepsService.delete(id)
            await vaultsService.getVaultKeeps(id)
          }

        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.masonry-with-columns {
  columns: 8 200px;
  column-gap: 1rem;
  div {
    display: inline-block;
    width: 100%;
  }
}
.container {
  position: relative;
  color: white;
}
.top-right {
  position: absolute;
  top: 8px;
  right: 16px;
}

.bottom-left {
  position: absolute;
  bottom: 8px;
  left: 16px;
}

.bottom-right {
  position: absolute;
  bottom: 8px;
  right: 16px;
}
// .card:hover {
//   transform: scale(1.05);
//   transition: 0.5s;
// }
// .card {
//   transition: 0.5s;
// }
.avatar {
  border-radius: 50%;
  height: 35px;
  width: 35px;
}
.keepname {
  text-shadow: 1px 1px 1px black;
}
.trash {
  text-shadow: 1px 1px 1px black;
}
</style>