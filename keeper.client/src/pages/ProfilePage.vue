<template>
  <div class="row p-5 align-items-center">
    <div class="col-2">
      <img class="rounded profilepic" :src="profile.picture" alt="" />
    </div>
    <div class="col-md-3 ms-md-3">
      <h2>{{ profile.name }}</h2>
      <h5 class="mt-md-3">Vaults: {{ vaults?.length }}</h5>
      <h5>Keeps: {{ keeps?.length }}</h5>
    </div>
    <div class="col-6 text-end">
      <button
        class="btn btn-primary buttons text-white shadow m-2"
        @click="newVault"
      >
        Create Vault
      </button>
      <button
        class="btn btn-primary buttons text-white shadow m-2"
        @click="newKeep"
      >
        Create Keep
      </button>
    </div>
  </div>
  <div class="row p-md-5 pb-0 pb-md-0">
    <div class="col-8">
      <h2>Vaults:</h2>
    </div>
  </div>
  <div class="row p-3">
    <div class="col-md-2 selectable" v-for="v in vaults" :key="v.id">
      <img
        class="img-fluid shadow rounded m-2 selectable"
        src="https://miro.medium.com/max/8064/1*2staFxgwAJSTRCTndoCEjw.jpeg"
        alt=""
        @click="getVaultById(v.id)"
      />
      <h6 class="ps-2 bottom-left text-white keepname">{{ v.name }}</h6>
    </div>
  </div>
  <div class="row p-md-5 pb-0">
    <h2>Keeps:</h2>
  </div>
  <div class="container">
    <div class="masonry-with-columns">
      <div
        class="bg-white m-3 rounded shadow card selectable"
        @click="setActive(k)"
        v-for="k in keeps"
        :key="k.id"
      >
        <img class="img img-fluid rounded" :src="k.img" alt="" />
        <div class="bottom-left">
          <h5 class="keepname">{{ k.name }}</h5>
        </div>
        <img
          class="img img-fluid avatar bottom-right mb-1"
          :src="k.creator.picture"
          alt=""
        />
      </div>
    </div>
  </div>
  <Modal id="keep-details">
    <template #modal-body><KeepDetails /></template>
  </Modal>
  <Modal id="vault-form">
    <template #modal-body><VaultForm /></template>
  </Modal>
  <Modal id="keep-form">
    <template #modal-body><KeepForm /></template>
  </Modal>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { onMounted } from "@vue/runtime-core";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { useRoute } from "vue-router";
import { profilesService } from "../services/ProfilesService";
import { vaultsService } from "../services/VaultsService";
import { Modal } from "bootstrap";
import { router } from "../router";
export default {
  setup() {
    const route = useRoute();
    onMounted(async () => {
      try {
        await profilesService.getProfile(route.params.id);
        await profilesService.getKeepsByProfile(route.params.id);
        await profilesService.getVaultsByProfile(route.params.id);
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    });
    return {
      profile: computed(() => AppState.profile),
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      activeKeep: computed(() => AppState.activeKeep),
      async setActive(keep) {
        try {
          AppState.activeKeep = keep
          Modal.getOrCreateInstance(document.getElementById("keep-details")).show();
          logger.log('active keep', keep)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },
      async getVaultById(id) {
        try {
          await vaultsService.getById(id)
          router.push({ name: "Vault", params: { id } })
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
          router.push({ name: "Home" })
        }
      },
      async newVault() {
        Modal.getOrCreateInstance(document.getElementById("vault-form")).show();
      },
      async newKeep() {
        Modal.getOrCreateInstance(document.getElementById("keep-form")).show();
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.profilepic {
  height: 200px;
  width: 200px;
}
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
.card:hover {
  transform: scale(1.05);
  transition: 0.5s;
}
.card {
  transition: 0.5s;
}
.avatar {
  border-radius: 50%;
  height: 35px;
  width: 35px;
}
.keepname {
  text-shadow: 1px 1px 1px black;
}
.buttons {
  text-shadow: 1px 1px 1px rgb(71, 71, 71);
}
</style>