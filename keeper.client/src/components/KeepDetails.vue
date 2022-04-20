<template>
  <div class="container-fluid d-flex">
    <div class="row main">
      <div class="col-md-6">
        <img class="image rounded pb-2" :src="activeKeep?.img" alt="" />
      </div>

      <div class="col-md-6">
        <div class="row justify-content-center align-items-center">
          <div class="col-2">
            <h5 class="m-2">
              <i class="mdi mdi-eye-outline text-primary" title="views"></i>
              {{ activeKeep.views }}
            </h5>
          </div>

          <div class="col-2">
            <h5
              class="
                m-2
                d-flex
                flex-row
                justify-content-center
                align-items-center
              "
            >
              <img class="icon me-2" src="src/assets/img/tinylogo.png" alt="" />
              {{ activeKeep?.kept }}
            </h5>
          </div>

          <div class="col-2">
            <h5 class="m-2">
              <i class="mdi mdi-share-variant text-primary" title="shares"></i>
              ?
            </h5>
          </div>

          <div class="row mt-md-5">
            <div class="col-12 text-center">
              <h2>{{ activeKeep?.name }}</h2>
            </div>
          </div>

          <div class="row mt-md-5 justify-content-center" style="height: 350px">
            <div class="col-9">
              {{ activeKeep?.description }}
            </div>
          </div>

          <div class="row mt-auto">
            <div class="col-5 text-center">
              <div v-if="account.id">
                <h6>Add to vault:</h6>
                <!-- TODO add v-model editable.value and v-for vaults -->
                <select v-model="vaultId">
                  <option v-for="v in vaults" :key="v.id" :value="v.id">
                    {{ v.name }}
                  </option>
                </select>
                <button
                  class="btn btn-primary p-1"
                  @click="addToVault(activeKeep.id)"
                >
                  +
                </button>
              </div>
            </div>
            <div class="col-2">
              <h3>
                <i
                  v-if="activeKeep.creatorId == account.id"
                  class="mdi mdi-delete-outline text-primary selectable"
                  title="delete"
                  @click="remove(activeKeep.id)"
                ></i>
              </h3>
            </div>
            <div class="col-5 text-center d-flex flex-row align-items-center">
              <img
                class="avatar img-fluid me-2 selectable"
                :src="activeKeep.creator?.picture"
                alt=""
                @click="goTo(activeKeep.creatorId)"
              />
              {{ activeKeep.creator?.name }}
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { useRoute, useRouter } from "vue-router"
import { Modal } from "bootstrap"
import { keepsService } from "../services/KeepsService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { onMounted, ref } from "@vue/runtime-core"
import { vaultKeepsService } from "../services/VaultKeepsService"
import { profilesService } from "../services/ProfilesService"
import { accountService } from "../services/AccountService"
import { vaultsService } from "../services/VaultsService"
export default {
  props: {
    activeKeep: {
      type: Object,
      required: true,
    },
    account: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const vaultId = ref('')
    const router = useRouter();
    const route = useRoute();
    onMounted(async () => {
      try {
        const account = AppState.account
        profilesService.getVaultsByProfile(account.id)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    });
    return {
      vaultId,
      activeKeep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.vaults),
      account: computed(() => AppState.account),
      route,
      goTo(creatorId) {
        router.push({ name: "Profile", params: { id: creatorId } })
        Modal.getOrCreateInstance(document.getElementById("keep-details")).hide();
      },
      remove(id) {
        try {
          keepsService.remove(id)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async addToVault(keepId) {
        try {
          const newVaultKeep = { keepId: keepId, vaultId: vaultId.value }
          logger.log('adding to vault', newVaultKeep)
          await vaultKeepsService.create(newVaultKeep)
          Pop.toast("added to vault!")
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
.debug .container,
.debug .container-fluid {
  outline: 2px double blue;
  outline-offset: -2px;
}

.debug .row {
  outline: 2px dashed red;
  outline-offset: -2px;
}

.debug [class*="col-"] {
  outline: 2px dotted forestgreen;
  outline-offset: -3px;
}

.image {
  height: 600px;
  width: 100%;
  object-fit: cover;
}
p {
  text-align: center;
}
.icon {
  height: 18px;
  width: 18px;
  display: block;
}
.main {
  // height: 80vh;
}
.avatar {
  height: 30px;
  width: 30px;
  border-radius: 50%;
}
</style>