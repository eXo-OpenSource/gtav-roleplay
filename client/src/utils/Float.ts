export module Float {
	export function parseFloat(str) {
		var float = 0,
		sign, order, mantissa, exp, int = 0,
		multi = 1
		if (/^0x/.exec(str)) {
			int = parseInt(str, 16)
		} else {
			for (var i = str.length - 1; i >= 0; i -= 1) {
				if (str.charCodeAt(i) > 255) {
					console.log("Wrong string parametr")
					return false
				}
				int += str.charCodeAt(i) * multi
				multi *= 256
			}
		}

		sign = (int >>> 31) ? -1 : 1
		exp = (int >>> 23 & 0xff) - 127
		mantissa = ((int & 0x7fffff) + 0x800000).toString(2)

		for (i = 0; i < mantissa.length; i += 1) {
			float += parseInt(mantissa[i]) ? Math.pow(2, exp) : 0
			exp--
		}

		return float * sign
	}

	export function read(buffer, offset) {
		const bytes = [
			buffer.ubyte(offset),
			buffer.ubyte(offset + 1),
			buffer.ubyte(offset + 2),
			buffer.ubyte(offset + 3),
		]

		const val =
			bytes[0]       |
			bytes[1] << 8  |
			bytes[2] << 16 |
			bytes[3] << 24

		return parseFloat("0x" + val.toString(16))
	}
}
