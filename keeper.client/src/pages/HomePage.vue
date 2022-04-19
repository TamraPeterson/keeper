<template>
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
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { keepsService } from "../services/KeepsService"
import { Modal } from "bootstrap"
import { vaultsService } from "../services/VaultsService"
import { accountService } from "../services/AccountService"
export default {
  name: 'Home',
  setup() {
    onMounted(async () => {
      try {
        await keepsService.getAll();
        await accountService.getAccount();
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps),
      activeKeep: computed(() => AppState.activeKeep),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      account: computed(() => AppState.account),
      async setActive(keep) {
        try {
          await keepsService.getById(keep.id)
          // await vaultsService.getVaultKeeps(keep.id) FIXME this needs a vault id in order to work
          Modal.getOrCreateInstance(document.getElementById("keep-details")).show();
          logger.log('active keep', keep)
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
.img {
  background: linear-gradient(to bottom, rgba(0, 0, 0, 0), rgba(0, 0, 0, 0.6));
}
</style>
