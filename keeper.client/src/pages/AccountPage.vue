<template>
  <div class="about">
    <div class="row p-5 align-items-center">
      <div class="col-2">
        <img class="rounded" :src="account.picture" alt="" />
      </div>
      <div class="col-3 ms-md-3">
        <h2>{{ account.name }}</h2>
        <h5 class="mt-md-3">Vaults: 0</h5>
        <h5>Keeps: 0</h5>
      </div>
    </div>
    <div class="row p-5">
      <div class="col">
        <h2>Vaults:</h2>
      </div>
    </div>
    <div class="row p-5">
      <div class="col">
        <h2>Keeps:</h2>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
export default {
  name: 'Account',

  setup(

  ) {
    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped>
img {
  height: 200px;
  width: 200px;
}
</style>
