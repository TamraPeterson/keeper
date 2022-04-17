<template>
  <div class="component">
    <div class="row p-md-5 align-items-center">
      <div class="col-md-6">
        <h1 class>{{ activeVault?.name }}:</h1>
        <h4>{{ activeVault.description }}</h4>
        <h5 class="mt-3">Keeps: {{ keeps.length }}</h5>
      </div>
      <div class="col-md-6 text-end">
        <button
          v-if="account.id == activeVault.creatorId"
          class="btn btn-primary"
        >
          Delete Vault
        </button>
      </div>
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
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { useRoute } from "vue-router"
import { onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
export default {

  setup() {
    const route = useRoute();
    onMounted(async () => {

      logger.log('found vault')

    })
    return {
      activeVault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account)
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
</style>