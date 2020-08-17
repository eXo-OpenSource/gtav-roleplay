module.exports = {
  theme: {
    extend: {
      alphaColors: ['gray.700']
    },
  },
  variants: {},
  plugins: [
    require('tailwindcss-bg-alpha')(),
  ]
}
