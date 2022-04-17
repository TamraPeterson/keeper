<template></template>


<script>
export default {
  props: {
    keep: {
      type: Object,
      required: true,
    }
  },
  setup(props) {
    return {}
  }
}
</script>


<style lang="scss" scoped>
.img {
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
</style>